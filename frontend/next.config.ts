import type { NextConfig } from "next";

const nextConfig: NextConfig = {

  images: {
    domains: ["images.unsplash.com", "via.placeholder.com"],
    },
    output: 'standalone', // This is important for Docker
    env: {
        NEXT_PUBLIC_API_URL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5149'
    },
    async rewrites() {
        return [
            {
                source: '/api/:path*',
                destination: `${process.env.API_URL || 'http://localhost:5149'}/api/:path*`,
            },
        ]
    },
};

export default nextConfig;
