import { useState } from "react";

const [page, setPage] = useState(5);
const totalPages = 10;
const maxVisiblePages = 3; // số trang trung tâm hiển thị (nằm giữa ...)

const createPageNumbers = () => {
  const pages = [];

  if (totalPages <= maxVisiblePages + 4) {
    // Nếu ít trang, hiển thị hết
    for (let i = 1; i <= totalPages; i++) pages.push(i);
  } else {
    pages.push(1); // Trang đầu

    if (page > 3) {
      pages.push("prevDots"); // Dấu ...
    }

    const start = Math.max(2, page - 1);
    const end = Math.min(totalPages - 1, page + 1);

    for (let i = start; i <= end; i++) {
      pages.push(i);
    }

    if (page < totalPages - 2) {
      pages.push("nextDots"); // Dấu ...
    }

    pages.push(totalPages); // Trang cuối
  }

  return pages;
};

const pageNumbers = createPageNumbers();
