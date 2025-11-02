"use client";

import { useState, useEffect } from "react";
import { useRouter, useParams } from "next/navigation";
import api from "@/lib/api";
import { Calendar } from "@/components/ui/calendar";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  CardDescription,
  CardFooter,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { Badge } from "@/components/ui/badge";
import { Clock, MapPin, ArrowLeft, Loader2 } from "lucide-react";
import { format } from "date-fns";
import { cn } from "@/lib/utils";

interface Doctor {
  id: number;
  fullName: string;
  specialization: string;
  biography: string;
  profilePictureUrl: string;
  availability: string;
}

export default function BookDoctorPage() {
  const router = useRouter();
  const { doctorId } = useParams();
  const [doctor, setDoctor] = useState<Doctor | null>(null);
  const [selectedDate, setSelectedDate] = useState<Date | undefined>();
  const [time, setTime] = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(false);
  const [fetchLoading, setFetchLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchDoctor = async () => {
      try {
        setFetchLoading(true);
        const { data } = await api.get<Doctor>(`/Doctors/${doctorId}`);
        setDoctor(data);
      } catch (err) {
        console.error(err);
        setError("Failed to load doctor information.");
      } finally {
        setFetchLoading(false);
      }
    };
    fetchDoctor();
  }, [doctorId]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedDate || !time || !description) {
      setError("Please fill in all fields.");
      return;
    }

    const dateTime = new Date(selectedDate);
    const [hours, minutes] = time.split(":").map(Number);
    dateTime.setHours(hours, minutes);

    setLoading(true);
    try {
      await api.post("/Appointments", {
        DoctorId: doctor?.id,
        DateTime: dateTime.toISOString(),
        Specialization: doctor?.specialization,
        Description: description,
      });
      router.push("/");
    } catch (err) {
      console.error(err);
      setError("Booking failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  if (fetchLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center space-y-4">
          <Loader2 className="h-8 w-8 animate-spin mx-auto text-blue-600" />
          <p className="text-gray-600">Loading doctor information...</p>
        </div>
      </div>
    );
  }
  console.log("doctoro: ", doctor?.profilePictureUrl);

  if (!doctor) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <Card className="w-full max-w-md text-center">
          <CardContent className="pt-6">
            <p className="text-red-500 mb-4">{error || "Doctor not found."}</p>
            <Button onClick={() => router.push("/doctors")}>
              Back to Doctors
            </Button>
          </CardContent>
        </Card>
      </div>
    );
  }

  return (
    <div className="min-h-screen py-8 px-4 sm:px-6 lg:px-8">
      <div className="max-w-4xl mx-auto">
        <Button
          variant="ghost"
          onClick={() => router.back()}
          className="mb-6 flex items-center gap-2 text-gray-600 hover:text-gray-900"
        >
          <ArrowLeft className="h-4 w-4" />
          Back
        </Button>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          <Card className="lg:col-span-1">
            <CardContent className="p-6">
              <div className="space-y-4">
                <div className="relative">
                  <img
                    src={doctor.profilePictureUrl}
                    alt={doctor.fullName}
                    className={cn(
                      "w-full h-64 object-cover rounded-lg",
                      "bg-gray-200"
                    )}
                    onError={(e) => {
                      const target = e.target as HTMLImageElement;
                      target.src = `https://via.placeholder.com/400x400/3B82F6/FFFFFF?text=${encodeURIComponent(
                        doctor.fullName.split(" ")[0]
                      )}`;
                    }}
                  />
                  <Badge
                    className="absolute top-3 left-3 bg-blue-600 hover:bg-blue-700"
                    variant="secondary"
                  >
                    {doctor.specialization}
                  </Badge>
                </div>

                <div className="space-y-3">
                  <h1 className="text-2xl font-bold text-gray-900">
                    {doctor.fullName}
                  </h1>

                  <div className="flex items-center gap-2 text-sm text-gray-600">
                    <Clock className="h-4 w-4" />
                    <span>{doctor.availability}</span>
                  </div>
                </div>

                <div className="text-sm text-gray-700 leading-relaxed">
                  {doctor.biography}
                </div>
              </div>
            </CardContent>
          </Card>

          <Card className="lg:col-span-2">
            <CardHeader>
              <CardTitle className="text-xl">Book Appointment</CardTitle>
              <CardDescription>
                Select a date and time for your consultation
              </CardDescription>
            </CardHeader>

            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div className="space-y-3">
                  <Label htmlFor="date" className="text-base">
                    Select Date
                  </Label>
                  <div className="border rounded-lg p-4 bg-white">
                    <Calendar
                      mode="single"
                      selected={selectedDate}
                      onSelect={setSelectedDate}
                      className="rounded-md border"
                      disabled={(date) => date < new Date()}
                    />
                  </div>
                </div>

                <div className="space-y-3">
                  <Label htmlFor="time" className="text-base">
                    Select Time
                  </Label>
                  <Input
                    id="time"
                    type="time"
                    value={time}
                    onChange={(e) => setTime(e.target.value)}
                    className="w-full"
                    required
                  />
                  <p className="text-sm text-gray-500">
                    Available hours: {doctor.availability}
                  </p>
                </div>

                <div className="space-y-3">
                  <Label htmlFor="description" className="text-base">
                    Consultation Details
                  </Label>
                  <Textarea
                    id="description"
                    placeholder="Please describe your symptoms, concerns, or reason for consultation..."
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="min-h-[120px] resize-none"
                    required
                  />
                  <p className="text-sm text-gray-500">
                    Provide as much detail as possible to help the doctor
                    prepare for your visit.
                  </p>
                </div>

                {error && (
                  <div className="p-3 bg-red-50 border border-red-200 rounded-lg">
                    <p className="text-red-700 text-sm">{error}</p>
                  </div>
                )}

                <Button
                  type="submit"
                  className="w-full h-12 text-base"
                  disabled={loading || !selectedDate || !time || !description}
                >
                  {loading ? (
                    <>
                      <Loader2 className="h-4 w-4 animate-spin mr-2" />
                      Booking Appointment...
                    </>
                  ) : (
                    "Confirm Appointment"
                  )}
                </Button>
              </form>
            </CardContent>
          </Card>
        </div>
      </div>
    </div>
  );
}
