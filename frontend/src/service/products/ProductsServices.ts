import axios  from '@/app/utils/axios';
// services/categoryService.ts



export async function fetchBooks(page : number , pageSize : number , genres = []) {
  try {
   

  

    const result = await axios.get("/Books/paging", { params: {
      page,
      pageSize,
      genre: genres.join(','),
    }, });
    return result.data;
  } catch (error) {
    console.error("Lỗi khi fetch sách:", error);
    throw error;
  }
}


export async function fetch8productnew() {
  try{
    const result = await axios.get("/Books/get8productnew"
  )
  return result.data;
  } catch (error) {
    console.error("Lỗi khi fetch sách:", error);
    throw error;
  }
  
}
export async function fetchBookDetail(id: string) {
  try {
    const result = await axios.get(`/Books/getproduct/${id}`);
    return result.data;
  } catch (error) {
    console.error("Lỗi khi fetch sách:", error);
    throw error;
  }
}
export async function fetchBookForHero() {
  try {
    const result = await axios.get(`/Books/getProductHero/`);
    return result.data;
  } catch (error) {
    console.error("Lỗi khi fetch sách:", error);
    throw error;
  }
}

export async function fetchRecomBook(bookid: string) {
  try {
    const result = await axios.get(`/Books/recomend/${bookid}`);
    return result.data;
  } catch (error) {
    console.error("Lỗi khi fetch sách:", error);
    throw error;
  }
}



