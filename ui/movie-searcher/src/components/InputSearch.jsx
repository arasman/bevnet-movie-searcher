import React, { useState } from 'react'
import PropTypes from 'prop-types'

export const InputSearch = ({onSearch, placeholder, buttonText}) => {
    const [inputValue, setInputValue] = useState('');
    const onInputChange = ({target}) => {
        setInputValue(target.value);
    }
    const onSubmit = (event) => {
        event.preventDefault();
        onSearch(inputValue);
    }
  return (
    <div id="inputSearch">
        <form onSubmit={onSubmit}>
            <input 
            type="text"
            placeholder={placeholder}
            value={inputValue}
            onChange={onInputChange}
            />
            <button type='submit'>{buttonText}</button>
        </form>
    </div>
  )
}

InputSearch.propTypes = {
    onSearch: PropTypes.func.isRequired,
    placeholder: PropTypes.string.isRequired,
    buttonText: PropTypes.string.isRequired
}

InputSearch.defaultProps = {
    placeholder: 'Search by movie title',
    buttonText: 'Search'
}