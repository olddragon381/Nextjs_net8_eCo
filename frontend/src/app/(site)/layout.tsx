"use client";
import { useState, useEffect } from "react";
import "../css/euclid-circular-a-font.css";
import "../css/style.css";
import { usePathname } from "next/navigation";
import { Poppins } from 'next/font/google';
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/redux/store";
import { getCurrentUser } from "@/redux/features/user-slice";

import { ReduxProvider } from "@/redux/provider";
import PreLoader from "@/components/Common/PreLoader";

import Header from "../../components/Header";
import Footer from "../../components/Footer";
import ScrollToTop from "@/components/Common/ScrollToTop";
import { CartModalProvider } from "../context/CartSidebarModalContext";
import { ModalProvider } from "../context/QuickViewModalContext";
import { PreviewSliderProvider } from "../context/PreviewSliderContext";
import QuickViewModal from "@/components/Common/QuickViewModal";
import CartSidebarModal from "@/components/Common/CartSidebarModal";
import PreviewSliderModal from "@/components/Common/PreviewSlider";
import { Toaster } from "react-hot-toast";
import CartSync from "@/components/Cart/CartSync";

const poppins = Poppins({
  subsets: ['latin-ext'],
  weight: ['300', '400', '500', '600', '700'],
  display: 'swap',
  variable: '--font-poppins',
});

function AppInit() {
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      dispatch(getCurrentUser());
    }
  }, [dispatch]);

  return null;
}

export default function RootLayout({ children }: { children: React.ReactNode }) {
  const [loading, setLoading] = useState(true);
  const pathname = usePathname();
  const isAdmin = pathname?.startsWith('/admin');

  useEffect(() => {
    setTimeout(() => setLoading(false), 1000);
  }, []);

  return (
    <html lang="vi" className={poppins.variable} suppressHydrationWarning>
      <body className="font-poppins">
        <ReduxProvider>
          <AppInit />
          {isAdmin ? (
            children // layout admin
          ) : loading ? (
            <PreLoader />
          ) : (
            <>
              <CartModalProvider>
                <ModalProvider>
                  <PreviewSliderProvider>
                    <CartSync />
                    <Toaster position="top-right" toastOptions={{ duration: 2000 }} />
                    <Header />
                    {children}
                    <QuickViewModal />
                    <CartSidebarModal />
                    <PreviewSliderModal />
                  </PreviewSliderProvider>
                </ModalProvider>
              </CartModalProvider>
              <ScrollToTop />
              <Footer />
            </>
          )}
        </ReduxProvider>
      </body>
    </html>
  );
}
