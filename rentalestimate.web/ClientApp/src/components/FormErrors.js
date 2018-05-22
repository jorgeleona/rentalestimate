import React from 'react';

export default function FormErrors ({formErrors}){
    return (
        <div className="alert alert-light" role="alert">
            {Object.keys(formErrors).map((fieldName, i) => {
                if(formErrors[fieldName].length > 0){
                    return (<p key={i} className="font-italic">{formErrors[fieldName]}</p>)
                } 
                else {return '';}
            }
            )}
        </div>
    );
}