import { selectCurrentUser } from "@/redux/features/user-slice";
import { addComment } from "@/service/rating/RatingService";
import { Rating } from "@/types/rating";
import { useState } from "react";
import { useSelector } from "react-redux";
import { showToast } from "../Common/ShowToaster";

const RatingForm = ({ bookid }) => {
  const [rating, setRating] = useState(0);
  const [comment, setComment] = useState("");
  const currentUser = useSelector(selectCurrentUser);

  const handleRating = (value) => {
    setRating(value);
  };

  const handlerRating = async (e) => {
    e.preventDefault();

    const ratingData = {
      bookId: bookid,
      score: rating,
      comment: comment,
    };

    try {
      var token = localStorage.getItem("token");
      await addComment(token,ratingData);
      setRating(0);
      setComment("");
      showToast("Đánh giá thành công", "success");
    } catch (error) {
      console.error("Lỗi khi gửi review:", error);
      showToast("Gửi đánh giá thất bại", "error");
    }
  };

  return (
    <div className="max-w-[550px] w-full">
      {currentUser ? (
        <form onSubmit={handlerRating}>
          <h2 className="font-medium text-2xl text-dark mb-3.5">Thêm review</h2>
          <div className="flex items-center gap-3 mb-7.5">
            <span>Xếp hạng của bạn*</span>
            <div className="flex items-center gap-1">
              {[1, 2, 3, 4, 5].map((star) => (
                <span
                  key={star}
                  onClick={() => handleRating(star)}
                  className={`cursor-pointer ${
                    star <= rating ? "text-[#FBB040]" : "text-gray-5"
                  }`}
                >
                  <svg
                className="fill-current"
                width="15"
                height="16"
                viewBox="0 0 15 16"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M14.6604 5.90785L9.97461 5.18335L7.85178 0.732874C7.69645 0.422375 7.28224 0.422375 7.12691 0.732874L5.00407 5.20923L0.344191 5.90785C0.0076444 5.9596 -0.121797 6.39947 0.137085 6.63235L3.52844 10.1255L2.72591 15.0158C2.67413 15.3522 3.01068 15.6368 3.32134 15.4298L7.54112 13.1269L11.735 15.4298C12.0198 15.5851 12.3822 15.3263 12.3046 15.0158L11.502 10.1255L14.8934 6.63235C15.1005 6.39947 14.9969 5.9596 14.6604 5.90785Z"
                  fill=""
                />
              </svg>


                </span>
              ))}
            </div>
            <span className="ml-2 text-sm text-gray-700">{rating}/5</span>
          </div>

          <div className="rounded-xl bg-white shadow-1 p-4 sm:p-6">
            <div className="mb-5">
              <label htmlFor="comments" className="block mb-2.5">Bình luận</label>
              <textarea
                name="comments"
                id="comments"
                rows={5}
                value={comment}
                onChange={(e) => setComment(e.target.value)}
                placeholder="Comment của bạn"
                className="rounded-md border border-gray-3 bg-gray-1 placeholder:text-dark-5 w-full p-5 outline-none duration-200 focus:border-transparent focus:shadow-input focus:ring-2 focus:ring-blue/20"
              ></textarea>
            </div>

            <button
              type="submit"
              className="inline-flex font-medium text-white bg-blue py-3 px-7 rounded-md ease-out duration-200 hover:bg-blue-dark"
            >
              Gửi Review
            </button>
          </div>
        </form>
      ) : (
        <div className="flex justify-center">
          <p className="text-red-500 mb-4">
            Bạn cần <a href="/signin" className="underline text-blue-600">đăng nhập</a> để gửi review.
          </p>
        </div>
      )}
    </div>
  );
};


export default RatingForm;
