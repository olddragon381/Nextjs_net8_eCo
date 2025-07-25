// AdminBookPage.tsx
"use client";

import React, { useEffect, useState } from "react";


import { showToast } from "@/components/Common/ShowToaster";
import { createBook, deleteBook, getBooksPaging, updateBook } from "@/service/Admin/AdminService";
import BookEditModal from "./BookEditModal";
import BookCreateModal from "./BookCreateModal";
import DeleteConfirmModal from "./DeleteConfirmModal ";

const AdminBookPage = () => {
  const [books, setBooks] = useState<any[]>([]);
  const [page, setPage] = useState(1);
  const [selectedBook, setSelectedBook] = useState<any | null>(null);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showCreateModal, setShowCreateModal] = useState(false);

  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

    const [bookToDelete, setBookToDelete] = useState(null);
const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

const handleDeleteClick = (book) => {
  setBookToDelete(book);
  setIsDeleteModalOpen(true);
};

const handleConfirmDelete = async () => {
  try {
    await deleteBook(token,bookToDelete.id); // gọi API xoá
    showToast("Xoá sách thành công", "success");
    fetchBooks(); // refresh danh sách
  } catch (err) {
    showToast("Xoá sách thất bại", "warning");
  } finally {
    setIsDeleteModalOpen(false);
    setBookToDelete(null);
  }
};

  const fetchBooks = async () => {
    try {
      const res = await getBooksPaging(page, 8);
      setBooks(res.items);
    } catch (err) {
      showToast("Lỗi khi tải danh sách sách", "warning");
    }
  };

  const handleEdit = (book: any) => {
    setSelectedBook(book);
    setShowEditModal(true);
  };

  const handleUpdate = async (bookId: string, updatedData: any) => {
    try {
      await updateBook(token, bookId, updatedData);
      showToast("Cập nhật sách thành công", "success");
   
      fetchBooks();
    } catch (err) {
      showToast("Cập nhật thất bại", "warning");
    }
    finally {
      setShowEditModal(false)
 
  }
  }

  const handleCreate = async (bookData: any) => {
    try {
      await createBook(token, bookData);
      showToast("Thêm sách thành công", "success");

      fetchBooks();
    } catch (err) {
      showToast("Thêm sách thất bại", "warning");
    }
    finally {
      setShowCreateModal(false)
 
  }
  };

  useEffect(() => {
    fetchBooks();
  }, [page]);

  return (
    <div className="p-6">
      <div className="flex justify-between mb-4">
        <h1 className="text-xl font-bold">Quản lý sách</h1>
        <button
          onClick={() => setShowCreateModal(true)}
          className="bg-blue text-white px-4 py-2 rounded"
        >
          Thêm sách
        </button>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full border rounded shadow text-sm">
          <thead className="bg-gray">
            <tr>
              <th className="px-4 py-2 text-left">Tên sách</th>
              <th className="px-4 py-2 text-left">Tác giả</th>
              <th className="px-4 py-2 text-left">Giá</th>
              <th className="px-4 py-2 text-left">Trạng thái</th>
              <th className="px-4 py-2 text-left">Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {books.map((book) => (
              <tr key={book.id} className="border-t hover:bg-gray-50">
                <td
                  className="px-4 py-2 text-blue-600 cursor-pointer"
                  onClick={() => handleEdit(book)}
                >
                  {book.title}
                </td>
                <td className="px-4 py-2">{book.authors}</td>
                <td className="px-4 py-2">{book.price}đ</td>
                <td className="px-4 py-2">{book.status}</td>
                <td className="px-4 py-2">
                  <button
                    onClick={() => handleEdit(book)}
                    className="bg-yellow text-white px-3 py-1 rounded"
                  >
                    Sửa
                  </button>
                  <button
  onClick={() => handleDeleteClick(book)}
  className="bg-red text-white px-3 py-1 rounded hover:bg-red-600"
>
  Xoá
</button>
                </td>
              </tr>
            ))}
            {books.length === 0 && (
              <tr>
                <td colSpan={5} className="text-center py-4 text-gray-500">
                  Không có sách.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      <div className="mt-4 flex justify-end gap-2">
        <button
          onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
          disabled={page === 1}
          className="px-3 py-1 bg-gray-200 rounded"
        >
          Trước
        </button>
        <button
          onClick={() => setPage((prev) => prev + 1)}
          className="px-3 py-1 bg-gray-200 rounded"
        >
          Sau
        </button>
      </div>

      {showEditModal && (
        <BookEditModal
          book={selectedBook}
          onClose={() => setShowEditModal(false)}
          onSave={handleUpdate}
        />
      )}
{isDeleteModalOpen && bookToDelete && (
  <DeleteConfirmModal
    isOpen={isDeleteModalOpen}
    bookTitle={bookToDelete.title}
    bookid={bookToDelete.id}
    onClose={() => setIsDeleteModalOpen(false)}
    onConfirm={handleConfirmDelete}
  />
)}
      {showCreateModal && (
        <BookCreateModal
          onClose={() => setShowCreateModal(false)}
          onSave={handleCreate}
        />
      )}
    </div>
  );
};

export default AdminBookPage;
