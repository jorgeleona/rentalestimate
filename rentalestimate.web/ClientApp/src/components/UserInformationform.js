class UserSubscription extends React.Component {

    state = { 
        data: this.props.initialData,
        statesList: this.props.statesList
    }

    loadCommentsFromServer = () => {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function() {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    };

    handleCommentSubmit = newuser => {

        var data = new FormData();
        data.append('firstname', newuser.firstname);
        data.append('lastname', newuser.lastname);
        data.append('phonenumber', newuser.phonenumber);
        data.append('email', newuser.email);
        data.append('address', newuser.address);
        data.append('zipcode', newuser.zipcode);
        data.append('city', newuser.city);
        data.append('statecode', newuser.statecode);

        var xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = function() {
            
        }.bind(this);
        xhr.send(data);
    };

    componentDidMount() {
        
    }

    render() {
        return (
            
            <div className="jumbotron jumbotron-fluid">
                <div className="container">
                    <h1 className="display-4">Estimate your annual rental</h1>
                    <p className="lead">Provide your basic contact information to estimate it</p>
                </div>
                <div>
                <UserInformationForm onCommentSubmit={this.handleCommentSubmit} StatesList={this.state.statesList} />
            </div>
            </div>
        )
    }
}

class UserInformationForm extends React.Component {
    state = {
        firstname: '',
        lastname: '',
        phonenumber: '',
        email: '',
        address: '',
        zipcode: '',
        city: '',
        statecode:'',
        formErrors: {
            firstname: '',
            lastname: '',
            phonenumber: '',
            email: '',
            address: '',
            zipcode: '',
            city: '',
            statecode:''
        },
        firstnameValid: false,
        lastnameValid: false,
        phonenumberValid: false,
        emailValid: false,
        addressValid: false,
        zipcodeValid: false,
        cityValid: false,
        statecodeValid: false,
        formValid: false
    }

    handleFieldChange = e => {

        const name = e.target.name;
        const value = e.target.value;
        this.setState({[name]: value}, this.validateField(name, value));
    };

    handleSubmit = e => {
        e.preventDefault();
        var firstname = this.state.firstname.trim();
        var lastname = this.state.lastname.trim();
        var phonenumber = this.state.phonenumber.trim();
        var email = this.state.email.trim();
        var address = this.state.address.trim();
        if (!firstname || !lastname) {
            return;
        }
        this.props.onCommentSubmit(this.state);
    };

    validateField(fieldName, value) {
        let fieldValidationErrors = this.state.formErrors;
        let field = fieldName+'Valid';
        let fieldValid = this.state[field];

        switch(fieldName) {
            case 'email':
                fieldValid = value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                fieldValidationErrors.email = fieldValid ? '' : ' is invalid';
                break;
        case 'phonenumber':
            fieldValid = value.match(/^(\()?\d{3}(\))?(-|\s)?\d{3}(-|\s)\d{4}$/);
            fieldValidationErrors.phonenumber = fieldValid ? '': ' must be a valid phone number';
            break;
        default:
            fieldValid = value.length > 0;
            fieldValidationErrors[fieldName] = fieldValid ? '': ' can not be blank';
            break;
    }
    
    this.setState({formErrors: fieldValidationErrors,
                    [field]: fieldValid,
                  }, this.validateForm());
    }
    
    validateForm() {
        let isFormValid = this.state.firstname && this.state.lastname &&
                                    this.state.phonenumber && this.state.email &&
                                    this.state.address && this.state.zipCode &&
                                    this.state.zipcode && this.state.city &&
                                    this.state.statecode;
        this.setState({formValid: isFormValid});

    }

    errorClass(error) {
        return(error.length === 0 ? '' : 'has-error');
    }

    render() {
        return (
            <form className="userinformationform" onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <label htmlFor="firstname">First Name</label>
                    <input className="form-control" id="firstname"
                        name="firstname" type="text" 
                        aria-describedby="Your First Name"
                        value={this.state.firstname}
                        onChange={this.handleFieldChange} />
                        <small id="firstnamevalidation" className="form-text text-muted">{this.state.firstnamevalidation}</small>
                </div>
                <div className="form-group">
                    <label htmlFor="lastname">Last Name</label>
                    <input className="form-control" id="lastname"
                         name="lastname" type="text" 
                         aria-describedby="Your Last Name" 
                        value={this.state.lastname}
                        onChange={this.handleFieldChange} />
                    <small id="lastnamevalidation" className="form-text text-muted">{this.state.lastnamevalidation}</small>
                </div>
                <div className="form-group">
                    <label htmlFor="phonenumber">Phone Number</label>
                    <input className="form-control" id="phonenumber"
                        name="phonenumber" type="text" 
                        aria-describedby="Phone number"
                        placeholder="Provide a valid phone number"
                        aria-describedby="Provide a valid email" 
                        value={this.state.phonenumber}
                        onChange={this.handleFieldChange} />
                </div>       
                <div className="form-group">
                    <label htmlFor="email">Email address</label>
                    <input type="email" className="form-control" id="email" 
                    name="email" 
                    aria-describedby="Provide a valid email" 
                    placeholder="Provide a valid email" 
                    value={this.state.email} 
                    onChange={this.handleFieldChange} />
                </div>
                <div className="form-group">
                    <label htmlFor="address">Address</label>
                    <input type="text" className="form-control" id="address" 
                    name="address" 
                    aria-describedby="Provide your complete address (number and street)" 
                    placeholder="Provide your complete address (number and street)" 
                    value={this.state.address} 
                    onChange={this.handleFieldChange} /> 
                </div>
                <div className="form-group">
                    <label htmlFor="city">City</label>
                    <input type="text" className="form-control" id="city"
                    name= "city"
                    aria-describedby="Provide your city" 
                    placeholder="Provide your city" 
                    value={this.state.city} 
                    onChange={this.handleFieldChange} /> 
                </div>
                <div className="form-group">
                    <label htmlFor="zipcode">Zip Code</label>
                    <input type="text" className="form-control" id="zipcode" 
                    name="zipcode" 
                    aria-describedby="Provide your zip code" 
                    placeholder="Provide your zip code" 
                    value={this.state.zipcode} 
                    onChange={this.handleFieldChange} /> 
                </div>
                <div className="form-group">
                    <label htmlFor="statecode">State</label>
                    <select className="form-control" id="statecode"
                        name="statecode" defaultValue="0"
                        value={this.state.stateCode}
                        onChange={this.handleFieldChange}>
                        {this.props.StatesList.map(x => <option key={x.id} value={x.code} label={x.name}>{x.name}</option>)}
                    </select>
                </div>
                <div className="panel panel-default">
                    <FormErrors formErrors={this.state.formErrors} />
                </div>
                <button type="submit" className="btn btn-primary">Save</button>
            </form>
        );
    }
}
