"use client";
import React, { use, useEffect, useState } from "react";
import Breadcrumb from "../Common/Breadcrumb";
import Image from "next/image";
import { useRouter } from "next/router";

import RecentlyViewdItems from "./RecentlyViewd";
import { usePreviewSlider } from "@/app/context/PreviewSliderContext";
import { AppDispatch, useAppSelector } from "@/redux/store";
import { fetchBookDetail, fetchRecomBook } from "@/service/products/ProductsServices";
import Link from "next/link";
import TagProductStatus from "../Common/TagStatusProduct";
import { useSyncCartItem } from "@/hooks/useSyncCartItem";
import { useDispatch, useSelector } from "react-redux";
import { addItemToCart } from "@/redux/features/cart-slice";
import { selectCurrentUser } from "@/redux/features/user-slice";
import RatingForm from "./RatingForm";
import CommentRating from "./CommentRating";

const ShopDetails = ({detail}) => {

  const { openPreviewModal } = usePreviewSlider();
  const [previewImg, setPreviewImg] = useState(0);
  const dispatch = useDispatch<AppDispatch>();
  
  const [quantity, setQuantity] = useState(1);

  const [activeTab, setActiveTab] = useState("tabOne");
  

  const product = detail;

  const [recomBooks, setRecomBooks] = useState([]);



  useEffect(() => {
    const getRecomBooks = async () => {
      try {
        const result = await fetchRecomBook(product.id);
        // nếu API trả về { data: [...] } thì:
        setRecomBooks(result ?? []);
        // hoặc nếu trả về mảng trực tiếp:
        // setRecomBooks(result ?? []);
      } catch (error) {
        console.error("Lỗi fetch sách gợi ý:", error);
        setRecomBooks([]);
      }
    };

    getRecomBooks();
  }, [product.id]);

  const tabs = [
    {
      id: "tabOne",
      title: "Mô tả",
    },
    {
      id: "tabTwo",
      title: "Additional Information",
    },
    {
      id: "tabThree",
      title: "Reviews",
    },
  ];




useSyncCartItem();
  const handleAddToCart = () => {
    dispatch(
      addItemToCart({
        ...product,
        id: product.id,
        quantity: quantity,

      })
    );
  };




  console.log(product);

  return (
    <>
      <Breadcrumb title={"Shop Details"} pages={["shop details"]} />

      {product.title === "" ? (
        "Please add product"
      ) : (
        <>
          <section className="overflow-hidden relative pb-20 pt-5 lg:pt-20 xl:pt-28">
            <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
              <div className="flex flex-col lg:flex-row gap-7.5 xl:gap-17.5">
                <div className="lg:max-w-[570px] w-full">
                  <div className="lg:min-h-[512px] rounded-lg shadow-1 bg-gray-2 p-4 sm:p-7.5 relative flex items-center justify-center">
                    <div>
                

                      <Image
                        src={product.image}
                        alt="products-details"
                        width={400}
                        height={400}
                      />
                    </div>
                  </div>

                  {/* ?  &apos;border-blue &apos; :  &apos;border-transparent&apos; */}
                  {/* <div className="flex flex-wrap sm:flex-nowrap gap-4.5 mt-6">
                    {product.imgs?.thumbnails.map((item, key) => (
                      <button
                        onClick={() => setPreviewImg(key)}
                        key={key}
                        className={`flex items-center justify-center w-15 sm:w-25 h-15 sm:h-25 overflow-hidden rounded-lg bg-gray-2 shadow-1 ease-out duration-200 border-2 hover:border-blue ${
                          key === previewImg
                            ? "border-blue"
                            : "border-transparent"
                        }`}
                      >
                        <Image
                          width={50}
                          height={50}
                          src={item}
                          alt="thumbnail"
                        />
                      </button>
                    ))}
                  </div> */}
                </div>

                {/* <!-- product content --> */}
                <div className="max-w-[539px] w-full">
                  <div className="flex items-center justify-between mb-3">
                    <h2 className="font-semibold text-xl sm:text-2xl xl:text-custom-3 text-dark">
                      {product.title}
                    </h2>

                    <div className="inline-flex font-medium text-custom-sm text-white bg-blue rounded py-0.5 px-2.5">
                      30% OFF
                    </div>
                  </div>

                  <div className="flex flex-wrap items-center gap-5.5 mb-4.5">
                    <div className="flex items-center gap-2.5">
                      {/* <!-- stars --> */}
                       <div className="flex items-center gap-1">
                                  <span>{product.rating}</span>
                                
                                  {[...Array(5)].map((_, index) => {
                                    const isFullStar = index + 1 <= Math.floor(product.rating);
                                    const isHalfStar =
                                      index + 1 > Math.floor(product.rating) &&
                                      index + 1 - product.rating < 1;
                                
                                    return (
                                      <Image
                                        key={index}
                                        src={
                                          isFullStar
                                            ? "/images/icons/icon-star.svg"
                                            : isHalfStar
                                            ? "/images/icons/icon-half-star.svg"
                                
                                
                                            : "/images/icons/icon-empty-star.svg"
                                        }
                                        alt="star icon"
                                        width={15}
                                        height={15}
                                      />
                                    );
                                  })}
                                </div>

                      <span>{product.ratingCount} đánh giá</span>
                    </div>

                    <TagProductStatus status={product.status}></TagProductStatus>

                  </div>

                  <h3 className="font-medium text-custom-1 mb-4.5">
                    <span className="text-sm sm:text-base text-dark">
                      Giá: {product.price+".000đ"}
                    </span>
                  
                  </h3>

                  {/* <ul className="flex flex-col gap-2">
                    <li className="flex items-center gap-2.5">
                      <svg
                        width="20"
                        height="20"
                        viewBox="0 0 20 20"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          d="M13.3589 8.35863C13.603 8.11455 13.603 7.71882 13.3589 7.47475C13.1149 7.23067 12.7191 7.23067 12.4751 7.47475L8.75033 11.1995L7.5256 9.97474C7.28152 9.73067 6.8858 9.73067 6.64172 9.97474C6.39764 10.2188 6.39764 10.6146 6.64172 10.8586L8.30838 12.5253C8.55246 12.7694 8.94819 12.7694 9.19227 12.5253L13.3589 8.35863Z"
                          fill="#3C50E0"
                        />
                        <path
                          fillRule="evenodd"
                          clipRule="evenodd"
                          d="M10.0003 1.04169C5.05277 1.04169 1.04199 5.05247 1.04199 10C1.04199 14.9476 5.05277 18.9584 10.0003 18.9584C14.9479 18.9584 18.9587 14.9476 18.9587 10C18.9587 5.05247 14.9479 1.04169 10.0003 1.04169ZM2.29199 10C2.29199 5.74283 5.74313 2.29169 10.0003 2.29169C14.2575 2.29169 17.7087 5.74283 17.7087 10C17.7087 14.2572 14.2575 17.7084 10.0003 17.7084C5.74313 17.7084 2.29199 14.2572 2.29199 10Z"
                          fill="#3C50E0"
                        />
                      </svg>
                      Free delivery available
                    </li>

                
                  </ul> */}

                 
                    <div className="flex flex-col gap-4.5 border-y border-gray-3 mt-7.5 mb-9 py-9">
                      {/* <!-- details item --> */}
                      <div className="flex items-center gap-4">
                        <div className="min-w-[65px]">
                          <h4 className="font-medium text-dark">Tác giả:</h4>
                           
                        </div>

                          <div className="flex items-center gap-4">
                          <span className="text-sm sm:text-base text-dark">
                     {product.authors}
                    </span>
                        </div>
                      </div>

                      {/* <!-- details item --> */}
                      <div className="flex items-center gap-4">
                        <div className="min-w-[65px]">
                          <h4 className="font-medium text-dark">Thể loại:</h4>
                        </div>
                          <div className="flex flex-wrap items-center gap-2">
  {product.genres.map((genre, index) => (
    <Link
      key={index}
      href={`/shop?genre=${encodeURIComponent(genre)}`}
      className="text-sm bg-gray-100 px-2 py-1 rounded-full text-dark hover:bg-blue-100 transition"
    >
      {genre}
    </Link>
  ))}
</div>
                        
                      </div>

                      
                    </div>

                    <div className="flex flex-wrap items-center gap-4.5">
                      <div className="flex items-center rounded-md border border-gray-3">
                        <button
                          aria-label="button for remove product"
                          className="flex items-center justify-center w-12 h-12 ease-out duration-200 hover:text-blue"
                          onClick={() =>
                            quantity > 1 && setQuantity(quantity - 1)
                          }
                        >
                          <svg
                            className="fill-current"
                            width="20"
                            height="20"
                            viewBox="0 0 20 20"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                          >
                            <path
                              d="M3.33301 10.0001C3.33301 9.53984 3.7061 9.16675 4.16634 9.16675H15.833C16.2932 9.16675 16.6663 9.53984 16.6663 10.0001C16.6663 10.4603 16.2932 10.8334 15.833 10.8334H4.16634C3.7061 10.8334 3.33301 10.4603 3.33301 10.0001Z"
                              fill=""
                            />
                          </svg>
                        </button>

                        <span className="flex items-center justify-center w-16 h-12 border-x border-gray-4">
                          {quantity}
                        </span>

                        <button
                          onClick={() => setQuantity(quantity + 1)}
                          aria-label="button for add product"
                          className="flex items-center justify-center w-12 h-12 ease-out duration-200 hover:text-blue"
                        >
                          <svg
                            className="fill-current"
                            width="20"
                            height="20"
                            viewBox="0 0 20 20"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                          >
                            <path
                              d="M3.33301 10C3.33301 9.5398 3.7061 9.16671 4.16634 9.16671H15.833C16.2932 9.16671 16.6663 9.5398 16.6663 10C16.6663 10.4603 16.2932 10.8334 15.833 10.8334H4.16634C3.7061 10.8334 3.33301 10.4603 3.33301 10Z"
                              fill=""
                            />
                            <path
                              d="M9.99967 16.6667C9.53944 16.6667 9.16634 16.2936 9.16634 15.8334L9.16634 4.16671C9.16634 3.70647 9.53944 3.33337 9.99967 3.33337C10.4599 3.33337 10.833 3.70647 10.833 4.16671L10.833 15.8334C10.833 16.2936 10.4599 16.6667 9.99967 16.6667Z"
                              fill=""
                            />
                          </svg>
                        </button>
                      </div>

                      <button
                        onClick={handleAddToCart}
                        className="inline-flex font-medium text-white bg-blue py-3 px-7 rounded-md ease-out duration-200 hover:bg-blue-dark"
                      >
                        Thêm vào giỏ
                      </button>

                      <a
                        href="#"
                        className="flex items-center justify-center w-12 h-12 rounded-md border border-gray-3 ease-out duration-200 hover:text-white hover:bg-dark hover:border-transparent"
                      >
                        <svg
                          className="fill-current"
                          width="24"
                          height="24"
                          viewBox="0 0 24 24"
                          fill="none"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path
                            fillRule="evenodd"
                            clipRule="evenodd"
                            d="M5.62436 4.42423C3.96537 5.18256 2.75 6.98626 2.75 9.13713C2.75 11.3345 3.64922 13.0283 4.93829 14.4798C6.00072 15.6761 7.28684 16.6677 8.54113 17.6346C8.83904 17.8643 9.13515 18.0926 9.42605 18.3219C9.95208 18.7366 10.4213 19.1006 10.8736 19.3649C11.3261 19.6293 11.6904 19.75 12 19.75C12.3096 19.75 12.6739 19.6293 13.1264 19.3649C13.5787 19.1006 14.0479 18.7366 14.574 18.3219C14.8649 18.0926 15.161 17.8643 15.4589 17.6346C16.7132 16.6677 17.9993 15.6761 19.0617 14.4798C20.3508 13.0283 21.25 11.3345 21.25 9.13713C21.25 6.98626 20.0346 5.18256 18.3756 4.42423C16.7639 3.68751 14.5983 3.88261 12.5404 6.02077C12.399 6.16766 12.2039 6.25067 12 6.25067C11.7961 6.25067 11.601 6.16766 11.4596 6.02077C9.40166 3.88261 7.23607 3.68751 5.62436 4.42423ZM12 4.45885C9.68795 2.39027 7.09896 2.1009 5.00076 3.05999C2.78471 4.07296 1.25 6.42506 1.25 9.13713C1.25 11.8027 2.3605 13.8361 3.81672 15.4758C4.98287 16.789 6.41022 17.888 7.67083 18.8586C7.95659 19.0786 8.23378 19.2921 8.49742 19.4999C9.00965 19.9037 9.55954 20.3343 10.1168 20.66C10.6739 20.9855 11.3096 21.25 12 21.25C12.6904 21.25 13.3261 20.9855 13.8832 20.66C14.4405 20.3343 14.9903 19.9037 15.5026 19.4999C15.7662 19.2921 16.0434 19.0786 16.3292 18.8586C17.5898 17.888 19.0171 16.789 20.1833 15.4758C21.6395 13.8361 22.75 11.8027 22.75 9.13713C22.75 6.42506 21.2153 4.07296 18.9992 3.05999C16.901 2.1009 14.3121 2.39027 12 4.45885Z"
                            fill=""
                          />
                        </svg>
                      </a>
                    </div>
                
                </div>
              </div>
            </div>
          </section>

          <section className="overflow-hidden bg-gray-2 py-20">
            <div className="max-w-[1170px] w-full mx-auto px-4 sm:px-8 xl:px-0">
              {/* <!--== tab header start ==--> */}
              <div className="flex flex-wrap items-center bg-white rounded-[10px] shadow-1 gap-5 xl:gap-12.5 py-4.5 px-4 sm:px-6">
                {tabs.map((item, key) => (
                  <button
                    key={key}
                    onClick={() => setActiveTab(item.id)}
                    className={`font-medium lg:text-lg ease-out duration-200 hover:text-blue relative before:h-0.5 before:bg-blue before:absolute before:left-0 before:bottom-0 before:ease-out before:duration-200 hover:before:w-full ${
                      activeTab === item.id
                        ? "text-blue before:w-full"
                        : "text-dark before:w-0"
                    }`}
                  >
                    {item.title}
                  </button>
                ))}
              </div>
              {/* <!--== tab header end ==--> */}

              {/* <!--== tab content start ==--> */}
              {/* <!-- tab content one start --> */}
              <div>
                <div
                  className={`flex-col sm:flex-row gap-7.5 xl:gap-12.5 mt-12.5 ${
                    activeTab === "tabOne" ? "flex" : "hidden"
                  }`}
                >
                  <div className="w-full">
                    <h2 className="font-medium text-2xl text-dark mb-7">
                      Mô tả:
                    </h2>

                    <p className="mb-6">
                      {product.description}

                    </p>
                    <p className="mb-6">
                      It has survived not only five centuries, but also the leap
                      into electronic typesetting, remaining essentially
                      unchanged. It was popularised in the 1960s.
                    </p>
                    
                  </div>

                  
                </div>
              </div>
              {/* <!-- tab content one end --> */}

              {/* <!-- tab content two start --> */}
              <div>
                <div
                  className={`rounded-xl bg-white shadow-1 p-4 sm:p-6 mt-10 ${
                    activeTab === "tabTwo" ? "block" : "hidden"
                  }`}
                >
                  
                  {/* <!-- info item --> */}
                  <div className="rounded-md even:bg-gray-1 flex py-4 px-4 sm:px-5">
                    <div className="max-w-[450px] min-w-[140px] w-full">
                      <p className="text-sm sm:text-base text-dark">Tên tác phẩm</p>
                    </div>
                    <div className="w-full">
                      <p className="text-sm sm:text-base text-dark">
                        {product.title}
                      </p>
                    </div>
                  </div>
                  {/* <!-- info item --> */}
                  <div className="rounded-md even:bg-gray-1 flex py-4 px-4 sm:px-5">
                    <div className="max-w-[450px] min-w-[140px] w-full">
                      <p className="text-sm sm:text-base text-dark">Tác giả</p>
                    </div>
                    <div className="w-full">
                      <p className="text-sm sm:text-base text-dark">
                        {product.authors}
                      </p>
                    </div>
                  </div>
                  {/* <!-- info item --> */}
                  <div className="rounded-md even:bg-gray-1 flex py-4 px-4 sm:px-5">
                    <div className="max-w-[450px] min-w-[140px] w-full">
                      <p className="text-sm sm:text-base text-dark">Số trang</p>
                    </div>
                    <div className="w-full">
                      <p className="text-sm sm:text-base text-dark">{product.numPages}</p>
                    </div>
                  </div>

                  

                 
                  {/* <!-- info item --> */}
                  <div className="rounded-md even:bg-gray-1 flex py-4 px-4 sm:px-5">
                    <div className="max-w-[450px] min-w-[140px] w-full">
                      <p className="text-sm sm:text-base text-dark">
                        Đánh giá
                      </p>
                    </div>
                    <div className="w-full">
                      <p className="text-sm sm:text-base text-dark">
                        {product.rating}
                      </p>
                    </div>
                  </div>
                  {/* <!-- info item --> */}
                  <div className="rounded-md even:bg-gray-1 flex py-4 px-4 sm:px-5">
                    <div className="max-w-[450px] min-w-[140px] w-full">
                      <p className="text-sm sm:text-base text-dark">Số review</p>
                    </div>
                    <div className="w-full">
                      <p className="text-sm sm:text-base text-dark">{product.reviewCount}</p>
                    </div>
                  </div>

                </div>
                    
                
              </div>

              
              {/* <!-- tab content two end --> */}

              {/* <!-- tab content three start --> */}
              <div>
                <div
                  className={`flex-col sm:flex-row gap-7.5 xl:gap-12.5 mt-12.5 ${
                    activeTab === "tabThree" ? "flex" : "hidden"
                  }`}
                >
                  <CommentRating bookid={product.id}></CommentRating>

                  <RatingForm bookid={product.id}></RatingForm>
                </div>
              </div>
              {/* <!-- tab content three end --> */}
              {/* <!--== tab content end ==--> */}
            </div>
          </section>

          <RecentlyViewdItems data = {recomBooks} />


        </>
      )}
    </>
  );
};

export default ShopDetails;
