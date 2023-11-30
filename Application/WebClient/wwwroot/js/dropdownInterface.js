let dropdowns = {};

export const initDropdown = (dropMenu, dropButton, uniqueId, options = {}) => {
  const $targetEl = document.getElementById(dropMenu);
  const $triggerEl = document.getElementById(dropButton);
  dropdowns[uniqueId] = new Dropdown($targetEl, $triggerEl, options);
}

export const hideDropdown = (uniqueId) => dropdowns[uniqueId].hide()

export const toggleDropdown = (uniqueId) => dropdowns[uniqueId].toggle()