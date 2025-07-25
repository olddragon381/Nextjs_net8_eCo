"use client";

import { useParams } from "next/navigation";
import { useEffect, useState } from "react";
import ShopDetails from "@/components/ShopDetails";
import { fetchBookDetail } from "@/service/products/ProductsServices";

const ShopDetailsPage = () => {
  const { id } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    if (!id) return;

    const fetchData = async () => {
      try {
        const res = await fetchBookDetail(id as string);
        setProduct(res);
      } catch (err) {
        console.error("Fetch error:", err);
      }
    };

    fetchData();
  }, [id]);

  if (!product) return <p>Đang tải sản phẩm...</p>;

  return <ShopDetails detail={product} />;
};

export default ShopDetailsPage;
