import { Card, Skeleton } from "@chakra-ui/react"

export function CardEntity({ imageSrc, title, price }) {
    return (
        // <Skeleton
        //     variant="shine"
        //     css={{
        //         "--start-color": "colors.gray.200",
        //         "--end-color": "colors.gray.300",
        //     }}>
        <Card.Root className="Card flex flex-col w-full">
            <Card.Header>
                <img src={imageSrc} className="card_image h-12 object-contain" alt="Card Image" />
            </Card.Header>
            <Card.Body>
                <h1 className="text-lg sm:text-sm font-semibold">{title}</h1>
            </Card.Body>
            <Card.Footer>
                <p className="text-gray-600">Price: {price}</p>
            </Card.Footer>
        </Card.Root>
        // </Skeleton>
    )
}
