/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        remotePatterns: [
            {
                protocol: "https",
                hostname: "media.istockphoto.com",
                port: "",
            },
            {
                protocol: "https",
                hostname: "my.depauw.edu",
                port: "",
            },
        ],
    },
};

export default nextConfig;
