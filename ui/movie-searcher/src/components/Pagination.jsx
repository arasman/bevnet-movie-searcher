import React from 'react'
import { SearchingContext, HasData, CurrentPage, Pages } from '../context/SearchingContext';

export const Pagination = ({onNext, onPrevious}) => {
    const currentResult = React.useContext(SearchingContext);

  return (
    <div id="pagination">
    {HasData(currentResult) && (
            <div>
                {(CurrentPage(currentResult) > 1 && <button onClick={onPrevious}>Go to page: {CurrentPage(currentResult)-1}</button>)}                
                {(CurrentPage(currentResult) < Pages(currentResult) && <button onClick={onNext}>Go to page: {CurrentPage(currentResult) +1}</button>)}
            </div>
      )}
  </div>
  )
}
