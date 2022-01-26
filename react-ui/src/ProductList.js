import React from 'react';
import { Container, Row } from 'react-bootstrap';
import Product from './Product';

class ProductList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: []
        };
    }

    render() {
        const { error, isLoaded, items } = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        } else {
            return (
                <Container>
                    {items.map(item => (
                        <Row key={item.id}><Product product={item} roles={this.props.roles}/></Row>
                    ))}
                </Container>
            )
        }

    }

    componentDidUpdate(prevProps, prevState, snapshot) {
        if(this.props.jwtToken != prevProps.jwtToken) {
            this.fetchProducts();
        }
    }

    componentDidMount() {
    }

    fetchProducts() {
        fetch("/product", {headers: {'Authorization': "Bearer " + this.props.jwtToken}})
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result
                    })
                }
            )
    }
}

ProductList.defaultProps = {
    roles: []
}

export default ProductList;