export const initialValue = {
    result: {
      page: 0,
      per_page: 0,
      total: 0,
      total_pages: 0,
      data: [],
    },
    error: false,
    message: "Success",
  };

export const getMovies = async (search, page) => {
    let data = {result: null, error: true, message: ''}
    try {    
      let url = "https://localhost:7139/movies/search"
      if (search != null && search.trim().length > 0 && page != null && page > 0)
          url = `${url}?title=${search}&page=${page}`;
      else if (search != null && search.trim().length > 0 && (page == null || (page != null && page <= 0)))
        url = `${url}?title=${search}`;
      else if ( (search == null ||  search != null && search.trim().length == 0) && page != null && page > 0) 
        url = `${url}?page=${page}`;

        const resp = await fetch(url); 
      if (resp.status == 200)
        data = await resp.json();
      else if (resp.status == 404)
        data = {result: { data: [] }, error: true, message: 'NoData'};
      else 
      data = {result: { data: [] }, error: true, message: 'InternalServerError'};
    }
    catch(e) {
      data = {result: { data: [] }, error: true, message: 'InternalServerError'};
    }
    return data;
}