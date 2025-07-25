"use client";
import { Swiper, SwiperSlide } from "swiper/react";
import { Autoplay, Pagination } from "swiper/modules";
import "swiper/css/pagination";
import "swiper/css";
import Image from "next/image";
import Link from "next/link";

const HeroCarousel = ({ data }) => {
  const books = data || [];
  return (
    <Swiper
      spaceBetween={30}
      centeredSlides={true}
      autoplay={{
        delay: 2500,
        disableOnInteraction: false,
      }}
      pagination={{
        clickable: true,
      }}
      modules={[Autoplay, Pagination]}
      className="hero-carousel"
    >
      {books.map((book, index) => (
        <SwiperSlide key={book.id || index}>
  <div className="flex flex-col-reverse sm:flex-row items-center justify-between gap-6 px-6 py-10">
    {/* LEFT: Thông tin sách */}
    <div className="max-w-xl sm:max-w-md lg:max-w-lg w-full">
      <div className="flex items-center gap-4 mb-4">
        <span className="font-semibold text-custom-sm text-blue">
          {book.authors || "Unknown Author"}
        </span>

    
      </div>

      <h1 className="font-semibold text-dark text-xl sm:text-2xl lg:text-3xl mb-3">
        <Link href={`/shop-details/${book.id}`}>{book.title}</Link>
      </h1>
        <div className="flex items-center gap-1">
          {[...Array(5)].map((_, i) => {
            const isFull = i + 1 <= Math.floor(book.rating);
            const isHalf = i + 1 > Math.floor(book.rating) && i + 1 - book.rating < 1;
            return (
              <Image
                key={i}
                src={
                  isFull
                    ? "/images/icons/icon-star.svg"
                    : isHalf
                    ? "/images/icons/icon-half-star.svg"
                    : "/images/icons/icon-empty-star.svg"
                }
                alt="star"
                width={15}
                height={15}
              />
            );
          })}
        </div>
      <p className="text-sm text-gray-700 leading-relaxed">
        {book.description
          ? book.description.length > 100
            ? `${book.description.slice(0, 100)}...`
            : book.description
          : "No description available."}
      </p>

      <Link
        href={`/shop-details/${book.id}`}
        className="inline-flex font-medium text-white text-custom-sm rounded-md bg-dark py-2.5 px-6 hover:bg-blue transition mt-6"
      >
        Shop Now
      </Link>
    </div>

    {/* RIGHT: Ảnh sách */}
    <div className="flex-shrink-0">
      <Image
        src={book.image || "/images/default-book.png"}
        alt={book.title}
        width={250}
        height={250}
        className="rounded-md object-contain"
      />
    </div>
  </div>
</SwiperSlide>

      ))}
    </Swiper>
  );
};

export default HeroCarousel;
