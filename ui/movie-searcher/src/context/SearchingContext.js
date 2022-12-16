import React from 'react'

export const SearchingContext = React.createContext({});

export const HasData = (currentResult) => {
    return (currentResult != null &&
        !currentResult.error &&
        (currentResult.result.data != null) &&
        (currentResult.result.data.length > 0));
}
export const NoData = (currentResult) => {
    return (currentResult != null &&
        currentResult.error &&
        currentResult.message == "NoData")
}
export const Data = (currentResult) => {
    if (HasData(currentResult))
        return currentResult.result.data;
    else return [];
}

export const CurrentPage = (currentResult) => {
    if (HasData(currentResult))
        return currentResult.result.page
    else return 0;
}

export const Pages = (currentResult) => {
    if (HasData(currentResult))
        return currentResult.result.total_pages;
    else return 0;
}
