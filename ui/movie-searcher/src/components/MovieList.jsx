import React from "react";
import { MovieListItem } from "./MovieListItem";
import { SearchingContext, HasData, Data } from "../context/SearchingContext";

export const MovieList = () => {
  const currentResult = React.useContext(SearchingContext);  
  return (
    <div id="movieList">
      {HasData(currentResult) && (
          <ol>
            {Data(currentResult).map((movie) => {
              return (
                <MovieListItem key={movie.imdbID} movie={movie}/>
              );
            })}
          </ol>
        )}
    </div>
  );
};
