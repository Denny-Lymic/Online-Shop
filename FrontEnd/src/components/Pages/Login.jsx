import { Box, Stack, Field, Input, Button, Center, Text, Spacer, StackSeparator } from '@chakra-ui/react'
import { useEffect } from 'react';
import { Form, Link as RouterLink, useActionData, useNavigate } from 'react-router';

export default function Login() {
    const actionData = useActionData();
    const navigate = useNavigate();

    useEffect(() => {
        if (actionData?.token) {
            navigate("/");
        }

    }, [actionData, navigate])

    return (
        <>
            <Center minH="15vh">
                <Text fontWeight="bold" textStyle="4xl" >Online-Shop</Text>
            </Center>

            <Center w="full" minH="65vh">

                <Box w={{ base: "90%", sm: "80%", md: "50%", lg: "40%" }} p="8" borderRadius="md" boxShadow="md" bg="white">
                    <Form method="POST">
                        <Stack gap="4">
                            <Text fontWeight="semibold" textStyle="2xl" mb="4">
                                Вход
                            </Text>

                            <Field.Root required invalid={!!actionData?.errors?.email || !!actionData?.errors?.invalidData}>
                                <Field.Label textStyle="lg">Email</Field.Label>
                                <Input
                                    type="email"
                                    name="email"
                                    placeholder="email@domain.com"
                                    onInvalid={(e) => (e.target.setCustomValidity("Введите корректный email (email@gmail.com)"))}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                />
                            </Field.Root>
                            <Field.Root required invalid={!!actionData?.errors?.password || !!actionData?.errors?.invalidData}>
                                <Field.Label textStyle="lg">Пароль</Field.Label>
                                <Input type="password" name="password" placeholder="••••••••" />
                            </Field.Root>
                            {actionData?.errors && (
                                <Box bg="red.200" p="3" borderRadius="md" visibility={!!actionData?.errors}>
                                    {actionData.errors.email && <Text>{actionData.errors.email}</Text>}
                                    {actionData.errors.password && <Text>{actionData.errors.password}</Text>}
                                    {actionData.errors.invalidData && <Text>{actionData.errors.invalidData}</Text>}
                                    {actionData.errors.server && <Text>{actionData.errors.server}</Text>}
                                </Box>)}

                            <Stack separator={<StackSeparator />} direction={{ base: "column", md: "row" }}>
                                <Button variant="ghost" as={RouterLink} to="/registration">
                                    Регистрация
                                </Button>
                                <Spacer />
                                <Button variant="ghost" as={RouterLink} to="/">
                                    Назад
                                </Button>
                            </Stack>
                            <Button type="submit" colorScheme="blue" width="full" mt="6" textStyle="lg" >
                                Войти
                            </Button>
                        </Stack>
                    </Form>
                </Box>
            </Center >
        </>
    )
}