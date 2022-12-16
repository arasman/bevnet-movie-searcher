import React from 'react'

export const MovieListItem = ({movie}) => {
  return (
    <li>{movie.year} - {movie.title}</li>
  )
}
