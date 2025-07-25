"use client";

import React, { useState, useEffect } from "react";
import { useRouter } from "next/navigation";

const CustomSelect = ({ options }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState(options[0]);
  const router = useRouter();

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  const handleOptionClick = (option) => {
    setSelectedOption(option);
    setIsOpen(false);

    // ✅ Chuyển trang đến /shop?genre=...
    router.push(`/shop?genre=${encodeURIComponent(option.value)}`);
  };


  useEffect(() => {
    function handleClickOutside(event) {
      if (!event.target.closest(".dropdown-content")) {
        setIsOpen(false); // 👈 dùng setIsOpen thay vì toggleDropdown()
      }
    }

    if (isOpen) {
      document.addEventListener("mousedown", handleClickOutside);
    }

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [isOpen]);

  return (
    <div className="dropdown-content custom-select relative" style={{ width: "200px" }}>
      <div
        className={`select-selected whitespace-nowrap ${
          isOpen ? "select-arrow-active" : ""
        }`}
        onClick={toggleDropdown}
      >
        {selectedOption?.label || "Chọn một mục"}
      </div>
      <div className={`select-items ${isOpen ? "" : "select-hide"}`}>
        {options.map((option, index) => (
          <div
            key={index}
            onClick={() => handleOptionClick(option)}
            className={`select-item ${
              selectedOption === option ? "same-as-selected" : ""
            }`}
          >
            {option.label}
          </div>
        ))}
      </div>
    </div>
  );
};


export default CustomSelect;
