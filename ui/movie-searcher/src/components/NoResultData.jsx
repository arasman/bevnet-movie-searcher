import React from "react";
import { SearchingContext, NoData } from "../context/SearchingContext";

export const NoResultData = () => {
  const currentResult = React.useContext(SearchingContext);
  return (
    <div id="noData">
      {NoData(currentResult) && (
            <p>
            There is no result that meets the current page and search
            filter.
          </p>
        )}
    </div>
  );
};
