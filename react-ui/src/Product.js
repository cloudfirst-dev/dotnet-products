import React from 'react';
import { BsHandThumbsUp } from "react-icons/bs";

class Product extends React.Component {
    render() {
        return (
            <div>
                {this.props.product.name}
                { this.props.roles.indexOf("vote") >= 0 ? <BsHandThumbsUp /> : null }
            </div>
        );
    }
}

Product.defaultProps = {
    roles: []
}

export default Product;