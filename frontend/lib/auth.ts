import api from "./api";
import { useAuth } from "@/context/AuthContext";

export function useAuthApi() {
  const { login, logout } = useAuth();

  const register = async (
    fullName: string,
    email: string,
    password: string
  ) => {
    const userName = fullName.replace(/\s+/g, "").toLowerCase();

    console.log("registering user", email);

    const res = await api.post("/Auth/register", {
      fullName,
      email,
      userName,
      password,
    });

    if (res.data?.token) {
      login(res.data.token);
    }

    return res.data;
  };

  const loginUser = async (email: string, password: string) => {
    console.log("logging in user", email);
    const res = await api.post("/Auth/login", { email, password });
    login(res.data.token);
  };

  return { register, loginUser, logout };
}
