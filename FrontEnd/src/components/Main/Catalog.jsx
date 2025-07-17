import { products } from '../../products.js'
import { CardEntity } from './Card.jsx'
import viteLogo from '/vite.svg'
import { Box } from '@chakra-ui/react'

export default function Catalog() {
    return (
        <Box className="w-5/6 h-auto grid grid-cols-3 sm:grid-cols-3 md:grid-cols-5 lg:grid-cols-7 gap-2">
            {products.map(product => (
                <CardEntity
                    key={product.Name}
                    className="Card"
                    imageSrc={viteLogo}
                    title={product.Name}
                    price={product.Price}
                />
            ))}
            {/* <CardEntity className="Card" imageSrc={viteLogo} title="My information" price="10000" />  */}
        </Box>
    )
}