import { useEffect, useState } from "react";
import { getMovies, initialValue } from "../api/Movies";

export const useMovies = () => {
  const [searchingResult, setSearchingResult] = useState(initialValue);
  const [searchText, setSearchText] = useState("");
  const [loading, setLoading] = useState(false);
  const fetchMovies = async (search, page) => {
    setLoading(true);
    const response = await getMovies(search, page);
    setSearchingResult(response);
    setLoading(false);
  };

  useEffect(() => {
    fetchMovies(null, null);
  }, []);

  const search = async (filterText) => {
    setSearchText(filterText);
    await fetchMovies(filterText, null);
  };

  const next = async () => {
    const currentPage = searchingResult.result.page;
    const maxPage = searchingResult.result.total_pages;
    if (currentPage < maxPage) {
        let nextPage = currentPage + 1;
      await fetchMovies(searchText, nextPage);
    }
  };

  const previous = async () => {
    const currentPage = searchingResult.result.page;
    if (currentPage > 1) {
        let prevPage = currentPage - 1;
        await fetchMovies(searchText, prevPage);
    }
  };

  return {
    loading,
    searchingResult,
    search,
    next,
    previous,
  };

};
