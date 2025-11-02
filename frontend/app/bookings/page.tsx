"use client";

import { useEffect, useState } from "react";
import api from "@/lib/api";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table";
import { format, parseISO } from "date-fns";
import { ArrowLeft, Trash2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
} from "@/components/ui/alert-dialog";

interface Appointment {
    id: number;
    dateISO: string;
    doctorId: number;
    doctor: { fullName: string; specialization: string };
    patientId: string;
    patientName: string;
    specialization: string;
    description: string;
}

export default function AppointmentsPage() {
    const [appointments, setAppointments] = useState<Appointment[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
    const [appointmentToDelete, setAppointmentToDelete] = useState<Appointment | null>(null);
    const [deleting, setDeleting] = useState(false);
    const router = useRouter();

    useEffect(() => {
        const fetchAppointments = async () => {
            try {
                const { data } = await api.get<Appointment[]>("/Appointments");
                const normalized = data.map((a: any) => ({
                    ...a,
                    dateISO: a.dateTime ?? a.date ?? a.date_time ?? null,
                }));
                setAppointments(normalized);
            } catch (err) {
                console.error(err);
                setError("Failed to load appointments.");
            } finally {
                setLoading(false);
            }
        };
        fetchAppointments();
    }, []);

    const handleDeleteClick = (appointment: Appointment) => {
        setAppointmentToDelete(appointment);
        setDeleteDialogOpen(true);
    };

    const handleDeleteConfirm = async () => {
        if (!appointmentToDelete) return;

        setDeleting(true);
        try {
            await api.delete(`/appointments/${appointmentToDelete.id}`);
            setAppointments(prev => prev.filter(a => a.id !== appointmentToDelete.id));
            setDeleteDialogOpen(false);
            setAppointmentToDelete(null);
        } catch (err) {
            console.error(err);
        } finally {
            setDeleting(false);
        }
    };

    const handleDeleteCancel = () => {
        setDeleteDialogOpen(false);
        setAppointmentToDelete(null);
    };

    if (loading) return <p className="text-center mt-10">Loading...</p>;
    if (error) return <p className="text-center mt-10 text-red-500">{error}</p>;

    return (
        <div className="min-h-screen p-4">
            <Button
                variant="ghost"
                onClick={() => router.push("/")}
                className="flex items-center gap-2 mb-4"
            >
                <ArrowLeft className="h-4 w-4" />
                Back to Home
            </Button>
            <Card className="w-full max-w-5xl mx-auto">
                <CardHeader>
                    <CardTitle>Appointments</CardTitle>
                </CardHeader>
                <CardContent>
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead>Date</TableHead>
                                <TableHead>Doctor</TableHead>
                                <TableHead>Patient</TableHead>
                                <TableHead>Specialization</TableHead>
                                <TableHead>Description</TableHead>
                                <TableHead>Actions</TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {appointments.map((appt) => (
                                <TableRow key={appt.id}>
                                    <TableCell>
                                        {appt.dateISO
                                            ? format(parseISO(appt.dateISO), "MMM d, yyyy h:mm a")
                                            : "—"}
                                    </TableCell>
                                    <TableCell>{appt.doctor.fullName}</TableCell>
                                    <TableCell>{appt.patientName}</TableCell>
                                    <TableCell>{appt.specialization}</TableCell>
                                    <TableCell>{appt.description}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="destructive"
                                            size="sm"
                                            onClick={() => handleDeleteClick(appt)}
                                            className="flex items-center gap-1"
                                        >
                                            <Trash2 className="h-4 w-4" />
                                            Delete
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </CardContent>
            </Card>

            <AlertDialog open={deleteDialogOpen} onOpenChange={setDeleteDialogOpen}>
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>Are you sure?</AlertDialogTitle>
                        <AlertDialogDescription>
                            This action cannot be undone. This will permanently delete the
                            appointment with {appointmentToDelete?.doctor.fullName} scheduled for{" "}
                            {appointmentToDelete?.dateISO
                                ? format(parseISO(appointmentToDelete.dateISO), "MMM d, yyyy 'at' h:mm a")
                                : "this date"}.
                        </AlertDialogDescription>
                    </AlertDialogHeader>
                    <AlertDialogFooter>
                        <AlertDialogCancel onClick={handleDeleteCancel}>
                            Cancel
                        </AlertDialogCancel>
                        <AlertDialogAction
                            onClick={handleDeleteConfirm}
                            disabled={deleting}
                            className="bg-destructive text-destructive-foreground hover:bg-destructive/90"
                        >
                            {deleting ? "Deleting..." : "Delete"}
                        </AlertDialogAction>
                    </AlertDialogFooter>
                </AlertDialogContent>
            </AlertDialog>
        </div>
    );
}