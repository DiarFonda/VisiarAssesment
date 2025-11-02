"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import api from "@/lib/api";

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardFooter,
} from "@/components/ui/card";
import ProtectedPage from "@/components/ProtectedPage";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import { ArrowLeft } from "lucide-react";

interface Doctor {
  id: number;
  fullName: string;
  specialization: string;
  biography?: string;
}

interface RecommendedDoctor {
  doctorId: number;
  name: string;
  specialization: string;
  score: number;
  matchReason: string;
}

export default function AppointmentsPage() {
  const router = useRouter();
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [search, setSearch] = useState("");
  const [modalOpen, setModalOpen] = useState(false);
  const [symptoms, setSymptoms] = useState("");
  const [recommendations, setRecommendations] = useState<RecommendedDoctor[]>(
    []
  );
  const [loadingRecommendations, setLoadingRecommendations] = useState(false);

  useEffect(() => {
    const fetchDoctors = async () => {
      try {
        const { data } = await api.get<Doctor[]>("/Doctors");
        setDoctors(data);
      } catch (err) {
        console.error(err);
      }
    };
    fetchDoctors();
  }, []);

  const filteredDoctors = doctors.filter(
    (d) =>
      d.fullName.toLowerCase().includes(search.toLowerCase()) ||
      d.specialization.toLowerCase().includes(search.toLowerCase())
  );

  const handleRecommend = async () => {
    if (!symptoms.trim()) return;
    setLoadingRecommendations(true);
    try {
      const { data } = await api.post<RecommendedDoctor[]>(
        "/Doctors/recommend",
        { symptoms }
      );
      setRecommendations(data);
    } catch (err) {
      console.error(err);
      setRecommendations([]);
    } finally {
      setLoadingRecommendations(false);
    }
  };

  return (
    <ProtectedPage>
      <div className="p-6 space-y-6">
        <Button
          variant="ghost"
          onClick={() => router.push("/")}
          className="flex items-center gap-2"
        >
          <ArrowLeft className="h-4 w-4" />
          Back to Home
        </Button>
        <Card className="w-full max-w-4xl mx-auto">
          <CardHeader className="flex justify-between items-center">
            <CardTitle className="text-2xl">Available Doctors</CardTitle>
            <Button onClick={() => setModalOpen(true)}>Recommend</Button>
          </CardHeader>
          <CardContent>
            <Input
              placeholder="Search doctors by name or specialty..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="mb-4"
            />

            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Name</TableHead>
                  <TableHead>Specialization</TableHead>
                  <TableHead>Actions</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {filteredDoctors.map((doctor) => (
                  <TableRow key={doctor.id}>
                    <TableCell>{doctor.fullName}</TableCell>
                    <TableCell>{doctor.specialization}</TableCell>
                    <TableCell>
                      <Button
                        variant="outline"
                        onClick={() =>
                          router.push(`/appointments/book/${doctor.id}`)
                        }
                      >
                        Book
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </CardContent>
        </Card>

        {/* Recommendation Modali*/}
        <Dialog open={modalOpen} onOpenChange={setModalOpen}>
          <DialogContent className="max-w-lg">
            <DialogHeader>
              <DialogTitle>Recommend a Doctor</DialogTitle>
            </DialogHeader>
            <CardContent className="space-y-4">
              <Input
                placeholder="Describe your symptoms..."
                value={symptoms}
                onChange={(e) => setSymptoms(e.target.value)}
              />
              <Button
                onClick={handleRecommend}
                disabled={loadingRecommendations}
              >
                {loadingRecommendations ? "Loading..." : "Get Recommendations"}
              </Button>

              {recommendations.length > 0 && (
                <div className="mt-4 space-y-2">
                  <h4 className="font-semibold">Recommended Doctors:</h4>
                  <ul className="space-y-1">
                    {recommendations.map((doc) => (
                      <li key={doc.doctorId} className="border p-2 rounded">
                        <div>
                          <strong>{doc.name}</strong> - {doc.specialization}
                        </div>
                        <div>Score: {doc.score.toFixed(2)}</div>
                        <div>Reason: {doc.matchReason}</div>
                        <Button
                          size="sm"
                          variant="outline"
                          className="mt-1"
                          onClick={() =>
                            router.push(`/appointments/book/${doc.doctorId}`)
                          }
                        >
                          Book this doctor
                        </Button>
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </CardContent>
            <DialogFooter>
              <Button variant="ghost" onClick={() => setModalOpen(false)}>
                Close
              </Button>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      </div>
    </ProtectedPage>
  );
}
