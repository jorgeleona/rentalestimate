import React, { Component } from 'react';

export class FetchData extends Component {
  displayName = FetchData.name

  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };

    fetch('api/Home/users')
      .then(response => response.json())
      .then(data => {
        this.setState({ users: data, loading: false });
      });
  }

  renderUsersTable(users) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>User Name</th>
            <th>Phone number</th>
            <th>EMail</th>
            <th>IP Address</th>
            <th>Address</th>
            <th>Rent Range</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user,i) => 
            <tr key={i}>
              <td>{user.firstName} {user.lastName}</td>
              <td>{user.phoneNumber}</td>
              <td>{user.eMail}</td>
              <td>{user.ipAddress}</td>
              <td>{user.address}, {user.city}, {user.stateCode} </td>
              <td>From {user.monthlyRangeLow} to {user.monthlyRangeHigh}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderUsersTable(this.state.users);

    return (
      <div>
        <h1>Users</h1>
        <p>List of users subscribed</p>
        {contents}
      </div>
    );
  }
}
