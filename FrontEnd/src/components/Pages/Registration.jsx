import { Box, Stack, Field, Input, Button, Center, Text, Spacer, StackSeparator } from '@chakra-ui/react'
import { useEffect, useState } from 'react';
import { Form, Link as RouterLink, useActionData, useNavigate } from 'react-router';

export default function Registration() {
    const actionData = useActionData();
    const [containerColor, setColor] = useState("");
    const navigate = useNavigate();

    useEffect(() => {
        if (actionData?.color) {
            setColor(actionData.color);
        }
        if (actionData?.success) {
            const timer = setTimeout(() => {
                navigate("/login");
            }, 3000);

            return () => clearTimeout(timer);
        }
    }, [actionData, navigate]);

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
                                Регистрация
                            </Text>

                            <Field.Root required invalid={!!actionData?.errors?.email}>
                                <Field.Label textStyle="lg">Email</Field.Label>
                                <Input
                                    type="email"
                                    name="email"
                                    placeholder="email@domain.com"
                                    onInvalid={(e) => (e.target.setCustomValidity("Введите корректный email (email@gmail.com)"))}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                />
                            </Field.Root>

                            <Field.Root required invalid={!!actionData?.errors?.userName}>
                                <Field.Label textStyle="lg">Ваше имя</Field.Label>
                                <Input
                                    type="text"
                                    name="userName"
                                    placeholder="Ваше имя"
                                    onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                />
                            </Field.Root>

                            <Field.Root required invalid={!!actionData?.errors?.password}>
                                <Field.Label textStyle="lg">Пароль</Field.Label>
                                <Input
                                    type="password"
                                    name="password"
                                    placeholder="••••••••"
                                    onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                />
                            </Field.Root>

                            <Field.Root required invalid={!!actionData?.errors?.password}>
                                <Field.Label textStyle="lg">Повторите пароль</Field.Label>
                                <Input
                                    type="password"
                                    name="passwordCheck"
                                    placeholder="••••••••"
                                    onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                    onInput={(e) => e.target.setCustomValidity("")

                                    }
                                />
                            </Field.Root>

                            {(actionData?.errors || actionData?.success) && (
                                <Box bg={containerColor} p="3" borderRadius="md" visibility={!!actionData?.errors}>
                                    {actionData.errors?.email && <Text>{actionData.errors.email}</Text>}
                                    {actionData.errors?.userName && <Text>{actionData.errors.userName}</Text>}
                                    {actionData.errors?.password && <Text>{actionData.errors.password}</Text>}
                                    {actionData.errors?.server && <Text>{actionData.errors.server}</Text>}
                                    {actionData.success?.message && <Text>{actionData.success.message}</Text>}
                                </Box>)}


                            <Stack separator={<StackSeparator />} direction={{ base: "column", md: "row" }}>

                                <Button as={RouterLink} to="/login" variant="ghost">
                                    Есть аккаунт?
                                </Button>

                                <Spacer />
                                <Button as={RouterLink} to="/" variant="ghost">
                                    Назад
                                </Button>
                            </Stack>
                            <Button type="submit" colorScheme="blue" width="full" mt="6" textStyle="lg" >
                                Подтвердить
                            </Button>
                        </Stack>
                    </Form>
                </Box>
            </Center >
        </>
    )
}