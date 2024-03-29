import React, { useState, useEffect } from 'react';
import "./modal.css"
function Modal({active, setActive, children}) {

    return (
        <div className={active ? "modal acttive" : "modal"} onClick={() => setActive(true)}>
            <div className={active ? "modal_content acttive" : "modal_content"} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}

export default Modal;