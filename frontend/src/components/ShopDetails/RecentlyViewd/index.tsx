"use client";
import React from "react";

import ProductItem from "@/components/Common/ProductItem";
import Image from "next/image";
import Link from "next/link";

import { Swiper, SwiperSlide } from "swiper/react";
import { useCallback, useRef } from "react";
import "swiper/css/navigation";
import "swiper/css";

const RecentlyViewdItems = ({ data }) => {
  if (!Array.isArray(data)) {
    console.error("RecentlyViewdItems expects an array but received:", data);
    return null; // hoặc return fallback UI
  }

  const products = data;

  const sliderRef = useRef(null);

  const handlePrev = useCallback(() => {
    if (!sliderRef.current) return;
    sliderRef.current.swiper.slidePrev();
  }, []);

  const handleNext = useCallback(() => {
    if (!sliderRef.current) return;
    sliderRef.current.swiper.slideNext();
  }, []);

  return (
    <section className="overflow-hidden pt-17.5">
      <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0 pb-15 border-b border-gray-3">
        <div className="swiper categories-carousel common-carousel">
          

          <Swiper
            ref={sliderRef}
            slidesPerView={4}
            spaceBetween={50}
            className="justify-between"
          >
            {products.map((item, key) => (
              <SwiperSlide key={key}>
                <ProductItem item={item} />
              </SwiperSlide>
            ))}
          </Swiper>
        </div>
      </div>
    </section>
  );
};

export default RecentlyViewdItems;
