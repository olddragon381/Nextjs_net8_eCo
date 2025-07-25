import Link from "next/link";
import React from "react";

const TagProductStatus = ({ status }) => {

      const statusMap = {
  0: { text: "In Stock", color: "text-green", iconColor: "#22AD5C" },
  1: { text: "Out of Stock", color: "text-red", iconColor: "#EF4444" },
  2: { text: "Coming Soon", color: "text-yellow", iconColor: "#F59E0B" },
  3: { text: "On Sale", color: "text-blue", iconColor: "#3B82F6" },
  4: { text: "Unavailable", color: "text-gray", iconColor: "#9CA3AF" },
};

const currentStatus = statusMap[status] || statusMap[4];

  return (
    <div className="flex items-center gap-1.5">
  <svg
    width="20"
    height="20"
    viewBox="0 0 20 20"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
  >
    <g clipPath="url(#clip0_375_9221)">
      <path
        d="M10 0.5625C4.78125 0.5625 0.5625 4.78125 0.5625 10C0.5625 15.2188 4.78125 19.4688 10 19.4688C15.2188 19.4688 19.4688 15.2188 19.4688 10C19.4688 4.78125 15.2188 0.5625 10 0.5625ZM10 18.0625C5.5625 18.0625 1.96875 14.4375 1.96875 10C1.96875 5.5625 5.5625 1.96875 10 1.96875C14.4375 1.96875 18.0625 5.59375 18.0625 10.0312C18.0625 14.4375 14.4375 18.0625 10 18.0625Z"
        fill={currentStatus.iconColor}
      />
      <path
        d="M12.6875 7.09374L8.9688 10.7187L7.2813 9.06249C7.00005 8.78124 6.56255 8.81249 6.2813 9.06249C6.00005 9.34374 6.0313 9.78124 6.2813 10.0625L8.2813 12C8.4688 12.1875 8.7188 12.2812 8.9688 12.2812C9.2188 12.2812 9.4688 12.1875 9.6563 12L13.6875 8.12499C13.9688 7.84374 13.9688 7.40624 13.6875 7.12499C13.4063 6.84374 12.9688 6.84374 12.6875 7.09374Z"
        fill={currentStatus.iconColor}
      />
    </g>
    <defs>
      <clipPath id="clip0_375_9221">
        <rect width="20" height="20" fill="white" />
      </clipPath>
    </defs>
  </svg>
  
  <span className={currentStatus.color}>{currentStatus.text}</span>
</div>
  );
};

export default TagProductStatus;

