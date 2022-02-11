import React from 'react';

class NewProduct extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            productName: ""
        };

        this.update = this.update.bind(this);
        this.add = this.add.bind(this);
    }

    update(event) {
        this.setState({productName: event.target.value});
    }

    add() {
        fetch("/product", {
            headers: {
                'Authorization': "Bearer " + this.props.jwtToken,
                'Content-Type': 'application/json'
            }, 
            method: "POST",
            body: JSON.stringify({name: this.state.productName})
        })
        .then(
            (result) => {
                this.setState({
                    productName: ""
                })

                this.props.fetchProducts();
            }
        );
    }

    render() {
        return (
            <div>
                <input type="text" value={this.state.productName} onChange={this.update} /> <button onClick={this.add}>Create</button>
            </div>
        );
    }
}

NewProduct.defaultProps = {
    jwtToken: null
}

export default NewProduct;