import React, { Component } from 'react';
import FormErrors from './FormErrors';
import {observable} from "mobx";
import {observer} from 'mobx-react';


const SubmitButton = observer(() => {
    return (
        <button type="submit" className="btn btn-primary" disabled={!FormInfo.formValid}>Save</button>
    );
    });

    const FormInfo = observable({
            firstname: '',
            lastname: '',
            phonenumber: '',
            email: '',
            address: '',
            zipcode: '',
            city: '',
            state:'',
            formErrors: {
                firstname: '',
                lastname: '',
                phonenumber: '',
                email: '',
                address: '',
                zipcode: '',
                city: '',
                state:''
            },
            firstnameValid: false,
            lastnameValid: false,
            phonenumberValid: false,
            emailValid: false,
            addressValid: false,
            zipcodeValid: false,
            cityValid: false,
            stateValid: false,
            formValid: false,
            formSubmitted: false,
            result:'',
            resultdetails:''
    }); 

export const Home = observer( class Home extends Component {
    displayName = Home.name

    constructor(props) {
        super(props);
        this.state = { 
            statesList: [],
            loadingStates : false,
            formSubmitted: false
        };

        this.handleFieldChange = this.handleFieldChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        fetch('api/Home/states')
            .then(response => response.json())
            .then(data => {
            this.setState({ statesList: data, loadingStates: false });
        });
    }

    resetForm(){
            FormInfo.firstname = '';
            FormInfo.lastname = '';
            FormInfo.phonenumber = '';
            FormInfo.email = '';
            FormInfo.address = '';
            FormInfo.zipcode = '';
            FormInfo.city = '';
            FormInfo.state = '';
            FormInfo.formErrors.firstname = '';
            FormInfo.formErrors.lastname = '';
            FormInfo.formErrors.phonenumber = '';
            FormInfo.formErrors.email = '';
            FormInfo.formErrors.address = '';
            FormInfo.formErrors.zipcode = '';
            FormInfo.formErrors.city = '';
            FormInfo.formErrors.state = '';
            FormInfo.firstnameValid = false;
            FormInfo.lastnameValid = false;
            FormInfo.phonenumberValid = false;
            FormInfo.emailValid = false;
            FormInfo.addressValid = false;
            FormInfo.zipcodeValid = false;
            FormInfo.cityValid = false;
            FormInfo.stateValid = false;
            FormInfo.formValid = false;
    }
    
    handleSubmit(e){
        e.preventDefault();
        FormInfo.formSubmitted = false;
        FormInfo.result='';
        FormInfo.resultdetails='';  
        let myHeaders = new Headers();

       let header = new Headers({
            'Access-Control-Allow-Origin':'*',
            'Content-Type': 'application/json'
        });

            fetch('api/Home/users/addUser', {
                method: 'post',
                mode: 'cors',
                headers: header,
                body: JSON.stringify({ 'FirstName': FormInfo.firstname.trim(),
                    'LastName': FormInfo.lastname.trim(),
                    'PhoneNumber': FormInfo.phonenumber.trim(),
                    'EMail': FormInfo.email.trim(),
                    'Address': FormInfo.address.trim(),
                    'ZipCode': FormInfo.zipcode.trim(),
                    'City': FormInfo.city.trim(),
                    'StateCode': FormInfo.state.trim()
                }),
            })
            .then(response => response.json())
            .then(response => {
                FormInfo.formSubmitted = true;
                FormInfo.result = response.Message;
                if(response.Success){
                    FormInfo.resultdetails = 'Rental range from ' + response.MonthlyRangeLow + ' to ' + response.MonthlyRangeHigh;
                }
                this.resetForm();
            }).catch(err => err);
        
    }

    handleFieldChange(e){
        let name = e.target.name;
        let value = e.target.value;
        FormInfo[name] = value;
        this.validateField(name, value);

     }
    
     validateField(fieldName, value) {
        let field = fieldName+'Valid';
        let fieldValid = false;

        switch(fieldName) {
            case 'email':
                fieldValid = value.trim().match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i);
                FormInfo.formErrors.email = fieldValid ? '' : 'Enter a valid email';
                break;
            case 'phonenumber':
                fieldValid = value.trim().match(/^(\()?\d{3}(\))?(-|\s)?\d{3}(-|\s)\d{4}$/);
                FormInfo.formErrors.phonenumber = fieldValid ? '' : 'Enter a valid phone number';
                break;
            case 'state':
                fieldValid = value !== "0";
                FormInfo.formErrors.state = fieldValid ? '' : 'Select a state';
                break;
            default:
                fieldValid = value.trim().length > 0;
                FormInfo.formErrors[fieldName] = fieldValid ? '': fieldName + ' can not be blank';
                break;
        }
        FormInfo[field]= fieldValid;
        this.validateForm();

    }

    validateForm() {
        let isFormValid = FormInfo.firstnameValid && 
                            FormInfo.lastnameValid &&
                            FormInfo.phonenumberValid && 
                            FormInfo.emailValid &&
                            FormInfo.addressValid && 
                            FormInfo.zipcodeValid &&
                            FormInfo.cityValid &&
                            FormInfo.stateValid;
        FormInfo.formValid=isFormValid;
    }

    renderStatesList(states) {
        return (
            <div className="form-group">
                <label htmlFor="state">State</label>
                    <select className="form-control" id="state"
                        name="state" 
                        value={FormInfo.state}
                        onChange={this.handleFieldChange}>
                        {states.map(x => <option key={x.id} value={x.code} label={x.name}>{x.name}</option>)}
                    </select>
                </div>
        );
    }

    render() {

        let statesSelect = this.state.loadingStates
        ? <p><em>Loading...</em></p>
        : this.renderStatesList(this.state.statesList);

        let resultSubmitted = FormInfo.formSubmitted    
        ? <div className="alert alert-light" role="alert">
            <p>{FormInfo.result}</p>
            <p>{FormInfo.resultdetails}</p>
        </div>
        :'';



        return (
            <div className="jumbotron jumbotron-fluid">
                <div className="container">
                    <h1 className="display-4">Estimate your annual rental</h1>
                    <p className="lead">Provide your basic contact information to estimate it</p>
                </div>
            <div>
            <form className="userinformationform" onSubmit={this.handleSubmit}>

                <div className="form-group">
                    <label htmlFor="firstname">First Name</label>
                    <input className="form-control" id="firstname"
                        name="firstname" type="text" 
                        aria-describedby="Your First Name"
                        value={FormInfo.firstname}
                        onChange={this.handleFieldChange} />
                </div>
                <div className="form-group">
                    <label htmlFor="lastname">Last Name</label>
                    <input className="form-control" id="lastname"
                         name="lastname" type="text" 
                         aria-describedby="Your Last Name" 
                        value={FormInfo.lastname}
                        onChange={this.handleFieldChange} />
                </div>
                <div className="form-group">
                    <label htmlFor="phonenumber">Phone Number</label>
                    <input className="form-control" id="phonenumber"
                        name="phonenumber" type="text" 
                        aria-describedby="Phone number"
                        placeholder="Provide a valid phone number XXX-XXX-XXXX "
                        value={FormInfo.phonenumber}
                        onChange={this.handleFieldChange} />
                </div>       
                <div className="form-group">
                    <label htmlFor="email">Email address</label>
                    <input type="email" className="form-control" id="email" 
                    name="email" 
                    aria-describedby="Provide a valid email" 
                    placeholder="Provide a valid email" 
                    value={FormInfo.email} 
                    onChange={this.handleFieldChange} />
                </div>
                <div className="form-group">
                    <label htmlFor="address">Address</label>
                    <input type="text" className="form-control" id="address" 
                    name="address" 
                    aria-describedby="Provide your complete address (number and street)" 
                    placeholder="Provide your complete address (number and street)" 
                    value={FormInfo.address} 
                    onChange={this.handleFieldChange} /> 
                </div>
                <div className="form-group">
                    <label htmlFor="city">City</label>
                    <input type="text" className="form-control" id="city"
                    name= "city"
                    aria-describedby="Provide your city" 
                    placeholder="Provide your city" 
                    value={FormInfo.city} 
                    onChange={this.handleFieldChange} /> 
                </div>
                <div className="form-group">
                    <label htmlFor="zipcode">Zip Code</label>
                    <input type="text" className="form-control" id="zipcode" 
                    name="zipcode" 
                    aria-describedby="Provide your zip code" 
                    placeholder="Provide your zip code" 
                    value={FormInfo.zipcode} 
                    onChange={this.handleFieldChange} /> 
                </div>
                {statesSelect}
                <div>
                    <FormErrors formErrors={FormInfo.formErrors} />
                </div>
                { <SubmitButton  />}
                <div>
                   {resultSubmitted}
                </div>

            </form>
        </div>
    </div>
    );
  }
})