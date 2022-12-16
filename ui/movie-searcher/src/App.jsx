import { InputSearch } from "./components/InputSearch";
import { MovieList } from "./components/MovieList";
import { NoResultData } from "./components/NoResultData";
import { Pagination } from "./components/Pagination";
import { SearchingContext } from "./context/SearchingContext";
import { useMovies } from "./hooks/useMovies";

function App() {
  const {searchingResult, search, next, previous} = useMovies();

  return (
    <div>
      <SearchingContext.Provider value={searchingResult}>
        <h1>Movie Searcher</h1>
        <InputSearch onSearch={search} placeholder="Search by movie title" buttonText="Search"/>
        <MovieList/>
        <Pagination onNext={next} onPrevious={previous}/>
        <NoResultData/>
      </SearchingContext.Provider>
    </div>
  );
}

export default App;
