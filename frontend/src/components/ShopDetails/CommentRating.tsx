import { useEffect, useState } from "react";
import { getComment, getCommentCount } from "@/service/rating/RatingService";
import { CommentRating as CommentRatingType } from "@/types/rating";
import Image from "next/image";

const CommentRating = ({ bookid }) => {
  const [commentCount, setCommentCount] = useState(0);
  const [dataComment, setDataComment] = useState<CommentRatingType[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const comments = await getComment(bookid);
        const count = await getCommentCount(bookid);
            console.log("Kết quả getComment:", comments); // 👈 THÊM DÒNG NÀY
   console.log("Kết quả count:", count); // 👈 THÊM DÒNG NÀY



        setDataComment(comments ?? []);
        setCommentCount(count ?? 0);
      } catch (error) {
        console.error("Lỗi fetch comment:", error);
      }
    };

    fetchData();
  }, [bookid]);

  return (
    <div className="max-w-[570px] w-full">
      <h2 className="font-medium text-2xl text-dark mb-9">
       Có {commentCount} Review{commentCount > 1 ? "s" : ""} cho sản phẩm
      </h2>

      <div className="flex flex-col gap-6">
        {dataComment.map((comment, index) => (
          <div key={index} className="rounded-xl bg-white shadow-1 p-4 sm:p-6">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-4">
                
                <div>
                  <h3 className="font-medium text-dark">{comment.fullName}</h3>
                  <p className="text-custom-sm text-gray-500">{comment.email}</p>
                </div>
              </div>

             

               <div className="flex items-center gap-1">
                                                <span>{comment.ratingValue}</span>
                                              
                                                {[...Array(5)].map((_, index) => {
                                                  const isFullStar = index + 1 <= Math.floor(comment.ratingValue);
                                                 
                                              
                                                  return (
                                                    <Image
                                                      key={index}
                                                      src={
                                                        isFullStar
                                                          ? "/images/icons/icon-star.svg"
                                                        
                                              
                                              
                                                          : "/images/icons/icon-empty-star.svg"
                                                      }
                                                      alt="star icon"
                                                      width={15}
                                                      height={15}
                                                    />
                                                  );
                                                })}
                                              </div>
            </div>

            <p className="text-dark mt-6">{comment.comment}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CommentRating;
