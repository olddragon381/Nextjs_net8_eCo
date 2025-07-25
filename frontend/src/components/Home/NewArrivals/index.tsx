"use client"
import { useEffect, useState } from "react";
import ProductItem from "@/components/Common/ProductItem";
import Link from "next/link";
import Image from "next/image";
import { fetch8productnew } from "@/service/products/ProductsServices";



const NewArrival = () => {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const result = await fetch8productnew();
        setBooks(result); // hoặc result.items nếu API trả về kiểu đó
      } catch (err) {
        console.error("Lỗi khi load sách mới:", err);
        setBooks([]);
      }
    };

    fetchBooks();
  }, []);

  return (
    <section className="overflow-hidden pt-15">
      <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
        {/* Title */}
        <div className="mb-7 flex items-center justify-between">
          <div>
            <span className="flex items-center gap-2.5 font-medium text-dark mb-1.5">
              {/* icon... */}
              Tuần này
            </span>
            <h2 className="font-semibold text-xl xl:text-heading-5 text-dark">
              Sản phẩm mới nhất
            </h2>
          </div>

          <Link
            href="/shop"
            className="inline-flex font-medium text-custom-sm py-2.5 px-7 rounded-md border-gray-3 border bg-gray-1 text-dark ease-out duration-200 hover:bg-dark hover:text-white hover:border-transparent"
          >
            View All
          </Link>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-x-7.5 gap-y-9">
          {books.map((item, key) => (
            <ProductItem item={item} key={key} />
          ))}
        </div>
      </div>
    </section>
  );
};

export default NewArrival;
