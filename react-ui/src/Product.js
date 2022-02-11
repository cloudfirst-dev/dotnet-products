import React from 'react';
import { BsHandThumbsUp, BsFillTrashFill } from "react-icons/bs";

class Product extends React.Component {
    render() {
        return (
            <div>
                {this.props.product.name}
                { this.props.roles.indexOf("vote") >= 0 ? <BsHandThumbsUp /> : null }
                { this.props.roles.indexOf("deleteProduct") >= 0 ? <BsFillTrashFill /> : null }
            </div>
        );
    }
}

Product.defaultProps = {
    roles: []
}

export default Product;