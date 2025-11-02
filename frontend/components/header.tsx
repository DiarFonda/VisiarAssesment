"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { useAuthApi } from "@/lib/auth";
import { useAuth } from "@/context/AuthContext";

export default function Header() {
  const [dropdownOpen, setDropdownOpen] = useState(false);
  //const router = useRouter();
  const { logout } = useAuthApi();
  const { isLoggedIn } = useAuth();

  return (
    <header className="flex justify-between items-center p-4 bg-gray-100">
      <div className="text-xl font-bold">VisiarAssesment</div>
      {isLoggedIn && (
        <div className="relative">
          <button
            className="w-10 h-10 rounded-full overflow-hidden border border-gray-300 cursor-pointer"
            onClick={() => setDropdownOpen(!dropdownOpen)}
          >
            <img
              src="/dummy.png"
              alt="Profile"
              className="w-full h-full object-cover"
            />
          </button>
          {dropdownOpen && (
            <div className="absolute right-0 mt-2 w-48 bg-white shadow-md border rounded z-50">
              <button
                className="w-full text-left px-4 py-2 hover:bg-gray-100"
                onClick={logout}
              >
                Logout
              </button>
            </div>
          )}
        </div>
      )}
    </header>
  );
}
