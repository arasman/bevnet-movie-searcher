import React, { useState } from 'react'

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
