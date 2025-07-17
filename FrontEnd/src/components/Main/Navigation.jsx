import User from './User'
import viteLogo from '/vite.svg'
import { Flex, Text, Link, Spacer, Drawer, Portal, Button, CloseButton, Stack, LinkBox, LinkOverlay } from '@chakra-ui/react'

export default function NavBar() {
    return (
        <>
            <Flex px={8} bg="gray.100" align="center" shadow="md" hideBelow="md">
                {/* Название проекта */}
                <Text fontSize="xl" fontWeight="bold" py={4}>Online-Shop</Text>

                {/* Навигационные ссылки */}
                <Flex gap={6} ml={10} align={"stretch"} py={4}>
                    <LinkBox><LinkOverlay href="#">Главная</LinkOverlay></LinkBox>
                    <LinkBox><LinkOverlay href="#">Каталог</LinkOverlay></LinkBox>
                    <LinkBox><LinkOverlay href="#">О нас</LinkOverlay></LinkBox>
                </Flex>

                <Spacer />

                <User />
            </Flex>

            <Drawer.Root placement={"left"} size={"sm"}>
                <Drawer.Trigger asChild>
                    <Button size="sm" ml="4" hideFrom="md" my="2">
                        "Бургер меню"
                    </Button>
                </Drawer.Trigger>
                <Portal>
                    <Drawer.Backdrop />
                    <Drawer.Positioner>
                        <Drawer.Content>
                            <Drawer.Header>
                                <Drawer.Title><Text fontSize="xl" fontWeight="bold">Online-Shop</Text></Drawer.Title>
                            </Drawer.Header>
                            <Drawer.Body>
                                <Stack>
                                    <Link href="#">Главная</Link>
                                    <Link href="#">Каталог</Link>
                                    <Link href="#">О нас</Link>
                                </Stack>
                            </Drawer.Body>
                            <Drawer.Footer>
                                <Drawer.ActionTrigger asChild>
                                    <Button variant="outline">Cancel</Button>
                                </Drawer.ActionTrigger>
                            </Drawer.Footer>

                            <Drawer.CloseTrigger asChild>
                                <CloseButton size="md" />
                            </Drawer.CloseTrigger>
                        </Drawer.Content>
                    </Drawer.Positioner>
                </Portal>
            </Drawer.Root>
        </>
    )
}
