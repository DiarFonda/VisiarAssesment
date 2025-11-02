"use client";

import Link from "next/link";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/context/AuthContext";

export default function HomePage() {
  const { isLoggedIn } = useAuth();

  console.log("isloggedIn", isLoggedIn);

  return (
    <main className="flex flex-col items-center justify-center h-screen text-center space-y-6">
      <h1 className="text-4xl font-bold">Welcome to Visiar Healthcare</h1>
      <p className="text-gray-500 max-w-md">
        Book appointments easily, manage your schedule, and get AI-powered
        doctor recommendations.
      </p>

      {isLoggedIn ? (
        <div className="space-x-4">
          <Link href="/bookings">
            <Button>View My Bookings</Button>
          </Link>
          <Link href="/appointments">
            <Button variant="outline">Book Now</Button>
          </Link>
        </div>
      ) : (
        <div className="space-x-4">
          <Link href="/login">
            <Button className="cursor-pointer">Login</Button>
          </Link>
          <Link href="/register">
            <Button className="cursor-pointer" variant="outline">
              Register
            </Button>
          </Link>
        </div>
      )}
    </main>
  );
}
