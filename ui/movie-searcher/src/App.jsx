import { InputSearch, MovieList, NoResultData , Pagination, Loader } from "./components";
import { SearchingContext } from "./context";
import { useMovies } from "./hooks";

function App() {
  const {loading, searchingResult, search, next, previous} = useMovies();

  return (
    <div>
      <SearchingContext.Provider value={searchingResult}>
        <h1>Movie Searcher</h1>
        <InputSearch onSearch={search} placeholder="Search by movie title" buttonText="Search"/>
        { (!loading) && <MovieList/>}
        { (!loading) && <Pagination onNext={next} onPrevious={previous}/>}
        { (!loading) &&<NoResultData/> }
        {(loading) && <Loader/>}
      </SearchingContext.Provider>
    </div>
  );
}

export default App;
