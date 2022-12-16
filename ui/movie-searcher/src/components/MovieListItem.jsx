import React from 'react'
import PropTypes from 'prop-types'

export const MovieListItem = ({movie}) => {
  return (
    <li>{movie.year} - {movie.title}</li>
  )
}

MovieListItem.propTypes = {
  movie: PropTypes.object.isRequired,
}