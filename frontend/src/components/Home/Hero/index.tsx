'use client'
import React, { useEffect, useState } from "react";
import HeroCarousel from "./HeroCarousel";
import HeroFeature from "./HeroFeature";
import Image from "next/image";
import { fetchBookForHero } from "@/service/products/ProductsServices";
import Link from "next/link";

type BookInHero = {
  id: string;
  title: string;
  image?: string;
  decription: string;
  authors: string;
  rating : number;
  status: number
  price : number
  createdad: Date

};


const Hero = () => {
  const [data, setData] = useState<BookInHero[]>([]); ;
  const [carouselBooks, setCarouselBooks] = useState<BookInHero[]>([]); ;
   const [sideBooks, setSideBooks] = useState<BookInHero[]>([]); ;

  useEffect(() => {
      const fetchBooks = async () => {
        try {
          const result = await fetchBookForHero();
          setData(result); // hoặc result.items nếu API trả về kiểu đó
          setCarouselBooks(result.slice(0,5))
          setSideBooks(result.slice(5,7))


        } catch (err) {
          console.error("Lỗi khi load sách mới:", err);
          setData([]);
          setCarouselBooks([]);
          setSideBooks([]);
        }
      };
  
      fetchBooks();
    }, []);
  


  return (
    <section className="overflow-hidden pb-10 lg:pb-12.5 xl:pb-15 pt-57.5 sm:pt-45 lg:pt-30 xl:pt-51.5 bg-[#E5EAF4]">
      <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
        <div className="flex flex-wrap gap-5">
          <div className="xl:max-w-[757px] w-full">
           
              <div className="relative z-1 rounded-[10px] bg-white overflow-hidden">
              {/* <!-- bg shapes --> */}
              <Image
                src="/images/hero/hero-bg.png"
                alt="hero bg shapes"
                className="absolute right-0 bottom-0 -z-1"
                width={534}
                height={520}
              />

              <HeroCarousel data={carouselBooks} />
            </div>
            

            
            
          </div>

          <div className="xl:max-w-[393px] w-full">
  <div className="flex flex-col sm:flex-row xl:flex-col gap-5">
    {sideBooks.map((book, index) => (
      <div
        key={book.id || index}
        className="w-ful xl:min-h-[235px] relative rounded-[10px] bg-white p-4 sm:p-6 flex items-center gap-4"
      >
        {/* LEFT: Thông tin sách */}
       <div className="flex-1 flex flex-col justify-center">
  <h2 className="font-semibold text-dark text-lg mb-2 leading-tight line-clamp-2">
    <Link href={`/shop-details/${book.id}`}>{book.title}</Link>
  </h2>
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

  <p className="font-medium text-dark-4 text-sm mb-1 line-clamp-1">
    {book.authors || "Unknown Author"}
  </p>

  <span className="font-semibold text-red text-lg">
    {book.price + ".000đ"}
  </span>
</div>


        {/* RIGHT: Hình ảnh sách */}
        <div className="flex-shrink-0">
          <Image
            src={book.image || "/images/default-book.png"}
            alt={book.title}
            width={90}
            height={120}
            className="rounded object-contain"
          />
        </div>
      </div>
    ))}
  </div>
</div>

        </div>
      </div>

      {/* <!-- Hero features --> */}
      <HeroFeature />
    </section>
  );
};

export default Hero;
