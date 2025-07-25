import axios  from '@/app/utils/axios';
import { Rating } from '@/types/rating';

export async function addComment(token, rating: Rating ) {
  try {
    const res = await axios.post(
    '/Comment/addrating',
    rating,
    {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })

    return res.data;
  } catch (error) {
    console.error("Lỗi khi gui comment:", error);
    throw error;
  }
}


export async function getComment(bookid: string) {
  try {
    const res = await axios.get(`/Comment/getcomment/${bookid}`)

    return res.data;
  } catch (error) {
    console.error("Lỗi khi lay comment:", error);
    throw error;
  }
}

export async function getCommentCount(bookid: string) {
  try {
    const res = await axios.get(`/Comment/getcommentcount/${bookid}`)

    return res.data;
  } catch (error) {
    console.error("Lỗi khi lay comment:", error);
    throw error;
  }
}