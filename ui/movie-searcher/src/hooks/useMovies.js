import { useEffect, useState } from "react";
import { getMovies, initialValue } from "../api/Movies";

export const useMovies = () => {
  const [searchingResult, setSearchingResult] = useState(initialValue);
  const [searchText, setSearchText] = useState("");

  const fetchMovies = async (search, page) => {
    const response = await getMovies(search, page);
    setSearchingResult(response);
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
    searchingResult,
    search,
    next,
    previous,
  };

};
