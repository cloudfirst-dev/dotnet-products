import logo from './logo.svg';
import './App.css';
import ProductList from './ProductList';
import { Container, Row, Col } from 'react-bootstrap';
import Jwt from './Jwt';
import React from 'react';

class App  extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      jwt: {
        iss: "dotnet-vm",
        sub: "abc",
        givenName: "John",
        surname: "Doe",
        email: "johndoe@example.com",
        roles: ["Manager"]
      },
      jwtToken: null,
      roles: [],
    }

    this.updateToken = this.updateToken.bind(this);
  }

  updateToken(jwtToken) {
    fetch("/roles", {headers: {'Authorization': "Bearer " + jwtToken}})
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            jwtToken,
            roles: result
          })
        }
      )
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <Container>
            <Row>
              <Col><ProductList jwtToken={this.state.jwtToken} roles={this.state.roles} /></Col>
              <Col><Jwt jwt={this.state.jwt} jwtSecret="qwertyuiopasdfghjklzxcvbnm123456" updateToken={this.updateToken} /></Col>
            </Row>
          </Container>
        </header>
      </div>
    );
  };
}

export default App;
