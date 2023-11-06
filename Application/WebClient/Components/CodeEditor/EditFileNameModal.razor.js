let fileNameModal;

export const initFileNameModal = () => {
  const $targetEl = document.getElementById('editFileNameModal')
  const options = {
    backdrop: 'static',
    backdropClasses: 'modalBackground',
  }
  fileNameModal = new Modal($targetEl, options)
}

export const toggleFileNameModal = () => fileNameModal.toggle()