/** @type {import('next').NextConfig} */
const nextConfig = {
 env: {
    NEXT_PUBLIC_API_BASE_URL: process.env.NEXT_PUBLIC_API_BASE_URL,
  },

  images: {
    domains: ["images.unsplash.com", "res.cloudinary.com","plus.unsplash.com","plus.unsplash.com","i.gr-assets.com"],
  },

};

module.exports = nextConfig;
