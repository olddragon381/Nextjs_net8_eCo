"use client";

import { useState } from "react";

const CategoryItem = ({ category, selected, onToggle }) => {
  return (
    <button
      className={`${
        selected ? "text-blue" : ""
      } group flex items-center justify-between ease-out duration-200 hover:text-blue`}
      onClick={() => onToggle(category.name, !selected)}
    >
      <div className="flex items-center gap-2">
        <div
          className={`cursor-pointer flex items-center justify-center rounded w-4 h-4 border ${
            selected ? "border-blue bg-blue" : "bg-white border-gray-3"
          }`}
        >
          <svg
            className={selected ? "block" : "hidden"}
            width="10"
            height="10"
            viewBox="0 0 10 10"
            fill="none"
          >
            <path
              d="M8.33317 2.5L3.74984 7.08333L1.6665 5"
              stroke="white"
              strokeWidth="1.94437"
              strokeLinecap="round"
              strokeLinejoin="round"
            />
          </svg>
        </div>
        <span>{category.name}</span>
      </div>
    </button>
  );
};


const CategoryDropdown = ({ categories, value = [], onChange }) => {
  const [toggleDropdown, setToggleDropdown] = useState(true);

  const handleToggleGenre = (genreName, isSelected) => {
    let updatedGenres = isSelected
      ? [...value, genreName]
      : value.filter((g) => g !== genreName);

    onChange?.(updatedGenres); // gửi ngược lên cha
  };

  return (
    <div className="bg-white shadow-1 rounded-lg">
      <div
        onClick={(e) => {
          e.preventDefault();
          setToggleDropdown(!toggleDropdown);
        }}
        className={`cursor-pointer flex items-center justify-between py-3 pl-6 pr-5.5 ${
          toggleDropdown && "shadow-filter"
        }`}
      >
        <p className="text-dark">Thể loại</p>
        <button
          aria-label="toggle dropdown"
          className={`text-dark ease-out duration-200 ${
            toggleDropdown && "rotate-180"
          }`}
        >
          {/* icon */}
        </button>
      </div>

      <div className={`flex-col gap-3 py-6 pl-6 pr-5.5 ${toggleDropdown ? "flex" : "hidden"}`}>
        {categories.map((category, index) => (
          <CategoryItem
            key={index}
            category={category}
            selected={value.includes(category.name)} // dùng từ props
            onToggle={handleToggleGenre}
          />
        ))}
      </div>
    </div>
  );
};

export default CategoryDropdown;
