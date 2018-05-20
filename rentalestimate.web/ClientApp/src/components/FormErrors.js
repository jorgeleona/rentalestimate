class FormErrors extends React.Component {
    state = { errorsList : this.props.formErrors}
    render(){
        return (
           <div className='formErrors'>
                {JSON.stringify(this.state.errorsList.data)}
            </div>
        );
    }
}
 
