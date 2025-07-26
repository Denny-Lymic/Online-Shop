import { Card, Text } from "@chakra-ui/react"

export function CardEntity({ imageSrc, title, price }) {
    return (
        <Card.Root
            className="Card flex flex-col w-full"
            transition="box-shadow 0.2s ease"
            _hover={{
                boxShadow: "0 0 7px 7px rgba(0, 0, 0, 0.2)", // готовая пресет-тень от Chakra
            }}
        >
            <Card.Header>
                <img src={imageSrc} className="card_image h-12 object-contain" alt="Card Image" />
            </Card.Header>
            <Card.Body>
                <Text
                    className="text-lg sm:text-sm font-semibold"
                    lineClamp={2}
                    lineHeight="1.2"
                    height="2.4em">
                    {title}
                </Text>
            </Card.Body>
            <Card.Footer>
                <p className="text-gray-600">Цена: {price} грн</p>
            </Card.Footer>
        </Card.Root>
    )
}
