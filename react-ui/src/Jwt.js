import React from 'react';
import CryptoJS from 'crypto-js';
import { Container, Row, Col } from 'react-bootstrap';

class Jwt extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            jwt: props.jwt,
            jwtToken: null,
            Manager: false,
            Admin: false,
        };
        this.props.updateToken(this.state.jwtToken);
        this.setJwt = this.setJwt.bind(this);
        this.setRoles = this.setRoles.bind(this);
        this.login = this.login.bind(this);
        this.logout = this.logout.bind(this);
    }

    login() {
        var state = {...this.state}
        state.jwtToken = this.signJwt(this.buildJwt(state.jwt), this.props.jwtSecret)

        this.setState(state);
        this.props.updateToken(state.jwtToken);
    }

    logout() {
        this.props.updateToken(null);
    }

    setJwt(event) {
        var state = {...this.state}
        state.jwt[event.target.name] = event.target.value;
        state.jwtToken = this.signJwt(this.buildJwt(state.jwt), this.props.jwtSecret)

        this.setState(state);
        this.props.updateToken(state.jwtToken);
    }

    setRoles(event) {
        var state = {...this.state}
        var exists = state.jwt.roles.indexOf(event.target.name);

        state[event.target.name] = event.target.checked;

        if(event.target.checked && exists < 0) {
            state.jwt.roles.push(event.target.name);
        } else {
            state.jwt.roles.splice(exists, 1);
        }

        state.jwtToken = this.signJwt(this.buildJwt(state.jwt), this.props.jwtSecret)
        this.setState(state);
        this.props.updateToken(state.jwtToken);
    }

    void(event) {}

    render() {
        return (
            <Container>
                <Row>
                    <Col>iss</Col>
                    <Col><input type="text" value={this.state.jwt.iss} name="iss" onChange={this.setJwt} /></Col>
                </Row>
                <Row>
                    <Col>sub</Col>
                    <Col><input type="text" value={this.state.jwt.sub} name="sub" onChange={this.setJwt} /></Col>
                </Row>
                <Row>
                    <Col>givenName</Col>
                    <Col><input type="text" value={this.state.jwt.givenName} name="givenName" onChange={this.setJwt} /></Col>
                </Row>
                <Row>
                    <Col>surname</Col>
                    <Col><input type="text" value={this.state.jwt.surname} name="surname" onChange={this.setJwt} /></Col>
                </Row>
                <Row>
                    <Col>email</Col>
                    <Col><input type="text" value={this.state.jwt.email} name="email" onChange={this.setJwt} /></Col>
                </Row>
                <Row>
                    <Col>roles</Col>
                    <Col>
                        Manager <input type="checkbox" checked={this.state.Manager} name="Manager" onChange={this.setRoles} /> <br/>
                        Admin <input type="checkbox" checked={this.state.Admin} name="Admin" onChange={this.setRoles} />
                    </Col>
                </Row>
                <Row>
                    <Col>Token</Col>
                    <Col><textarea value={this.state.jwtToken} onChange={this.void}/></Col>
                </Row>
                <Row>
                    <Col><button type="button" onClick={this.login}>Login</button></Col>
                    <Col><button type="button" onClick={this.logout}>Logout</button></Col>
                </Row>
            </Container>
        );
    }

    signJwt(token, secret) {
        var signature = CryptoJS.HmacSHA256(token, secret);
        signature = this.base64url(signature);

        var signedToken = token + "." + signature;

        return signedToken;
    }

    buildJwt(data) {
        var header = {
            "alg": "HS256",
            "typ": "JWT"
        };
        
        var stringifiedHeader = CryptoJS.enc.Utf8.parse(JSON.stringify(header));
        var encodedHeader = this.base64url(stringifiedHeader);
        
        var stringifiedData = CryptoJS.enc.Utf8.parse(JSON.stringify(data));
        var encodedData = this.base64url(stringifiedData);
        
        var token = encodedHeader + "." + encodedData;

        return token;
    }

    base64url(source) {
        // Encode in classical base64
        var encodedSource = CryptoJS.enc.Base64.stringify(source);

        // Remove padding equal characters
        encodedSource = encodedSource.replace(/=+$/, '');

        // Replace characters according to base64url specifications
        encodedSource = encodedSource.replace(/\+/g, '-');
        encodedSource = encodedSource.replace(/\//g, '_');

        return encodedSource;
    }
}

export default Jwt;