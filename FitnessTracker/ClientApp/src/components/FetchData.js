import React, { Component } from 'react';
import Api from "../Api";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { 
        loginResponse: {
            token: "",
            refreshToken: ""
        }
    };
  }

  componentDidMount() {
    this.loginUser();
  }

  static renderUserLoginData(loginResponse) {
    return (
        <div>
            <h3>Token</h3>
            <p>{loginResponse.token}</p>
            <h3>RefreshToken</h3>
            <p>{loginResponse.refreshToken}</p>
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderUserLoginData(this.state.loginResponse);

    return (
      <div>
        <h1 id="tabelLabel" >Example Component</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async loginUser() {

     await Api.post('/auth/login', {
         email: "user@gmail.com",
         password: "Password#2!"
     })
          .then(response => {
              this.setState({ loginResponse: response.data, loading: false });
          })
         .catch(errors => {
             console.log(errors);
         })
  }
}
